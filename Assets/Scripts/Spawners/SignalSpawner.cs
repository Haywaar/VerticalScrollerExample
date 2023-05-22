using System;
using CustomEventBus;
using CustomEventBus.Signals;
using Cysharp.Threading.Tasks;
using Interactables;
using UnityEngine;

/// <summary>
/// Крутит таймеры и отправляет сигналы на спавн разных сущностей
/// </summary>
public class SignalSpawner : MonoBehaviour, IService
{
    private bool _isLevelRunning = false;
    private Level _level;

    private float _curTime;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<SetLevelSignal>(LevelSet);
        _eventBus.Subscribe<GameStartedSignal>(GameStart);
        _eventBus.Subscribe<GameStopSignal>(GameStop);
    }

    private void LevelSet(SetLevelSignal signal)
    {
        _level = signal.Level;
    }

    private void GameStart(GameStartedSignal signal)
    {
        _isLevelRunning = true;

        _curTime = 0f;

        var interactables = _level.InteractableData;

        foreach (var interactableData in interactables)
        {
            SpawnInteractable(interactableData);
        }

        TrackTime();
    }

    private async UniTask SpawnInteractable(InteractableData interactableData)
    {
        var cooldown = interactableData.StartCooldown;

        await UniTask.Delay(TimeSpan.FromSeconds(interactableData.PrewarmTime));
        while (_isLevelRunning)
        {
            _eventBus.Invoke(new SpawnInteractableSignal(interactableData.InteractablePrefab));

            await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
            cooldown = Mathf.Lerp(interactableData.StartCooldown,
                interactableData.EndCooldown, (_curTime / _level.LevelLength));
        }
    }

    private void GameStop(GameStopSignal signal)
    {
        _isLevelRunning = false;
    }

    private async UniTask TrackTime()
    {
        while (_isLevelRunning)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
            _curTime += 0.1f;

            var levelProgress = _curTime / _level.LevelLength;

            _eventBus.Invoke(new LevelProgressChangedSignal(levelProgress));

            if (_curTime >= _level.LevelLength)
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