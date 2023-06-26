using System;
using CustomEventBus;
using CustomEventBus.Signals;
using Cysharp.Threading.Tasks;
using Interactables;
using UnityEngine;

/// <summary>
/// Крутит таймеры, отправляет сигналы на спавн сущностей
/// Также отправляет сигнал когда время уровня закончилось
/// </summary>
public class SignalSpawner : MonoBehaviour, IService
{
    private bool _isLevelRunning = false;
    private LevelData _levelData;

    private float _curTime;

    private EventBus _eventBus;

    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<SetLevelSignal>(LevelSet);
        _eventBus.Subscribe<GameStartedSignal>(GameStart);
        _eventBus.Subscribe<GameStopSignal>(GameStop);
    }

    private void LevelSet(SetLevelSignal signal)
    {
        _levelData = signal.LevelData;
    }

    private void GameStart(GameStartedSignal signal)
    {
        _isLevelRunning = true;

        _curTime = 0f;

        var interactables = _levelData.InteractableData;

        foreach (var interactableData in interactables)
        {
            SpawnInteractable(interactableData);
        }

        TrackLevelProgress();
    }

    private async UniTask SpawnInteractable(InteractableSpawnData interactableSpawnData)
    {
        var cooldown = interactableSpawnData.StartCooldown;

        await UniTask.Delay(TimeSpan.FromSeconds(interactableSpawnData.PrewarmTime));
        while (_isLevelRunning)
        {
            _eventBus.Invoke(new SpawnInteractableSignal(interactableSpawnData.InteractableType, interactableSpawnData.InteractableGrade));

            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            cooldown = Mathf.Lerp(interactableSpawnData.StartCooldown,
                interactableSpawnData.EndCooldown, (_curTime / _levelData.LevelLength));
        }
    }

    private void GameStop(GameStopSignal signal)
    {
        _isLevelRunning = false;
    }

    private async UniTask TrackLevelProgress()
    {
        while (_isLevelRunning)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            _curTime += 0.1f;

            var levelProgress = _curTime / _levelData.LevelLength;

            _eventBus.Invoke(new LevelProgressChangedSignal(levelProgress));

            if (_curTime >= _levelData.LevelLength)
            {
                _eventBus.Invoke(new LevelTimePassedSignal());
                _isLevelRunning = false;
            }
        }
    }

    private void OnDestroy()
    {
        _isLevelRunning = false;

        _eventBus.Unsubscribe<SetLevelSignal>(LevelSet);
        _eventBus.Unsubscribe<GameStartedSignal>(GameStart);
        _eventBus.Unsubscribe<GameStopSignal>(GameStop);
    }
}