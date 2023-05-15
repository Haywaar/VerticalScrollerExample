using System;
using Cysharp.Threading.Tasks;
using Examples.VerticalScrollerExample.NegativeInteractables;
using Examples.VerticalScrollerExample.PositiveInteractable;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    /// <summary>
    /// Крутит таймеры и отправляет сигналы на спавн разных сущностей
    /// </summary>
    public class SignalSpawner : MonoBehaviour, IService
    {
        private bool _isLevelRunning = false;
        private Level _level;

        private float _curTime;

        private void Awake()
        {
            EventBus.Instance.LevelSet += LevelSet;
            EventBus.Instance.GameStop += LevelStop;
            EventBus.Instance.GameStart += GameStart;
        }

        private void LevelSet(Level level)
        {
            _level = level;
        }

        private void GameStart()
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
                EventBus.Instance.SpawnInteractable?.Invoke(interactableData.InteractablePrefab);
                
                await UniTask.Delay(TimeSpan.FromSeconds(cooldown));
                cooldown = Mathf.Lerp(interactableData.StartCooldown,
                    interactableData.EndCooldown, (_curTime / _level.LevelLength));
            }
        }

        private void LevelStop()
        {
            _isLevelRunning = false;
        }

        private async UniTask TrackTime()
        {
            while (_isLevelRunning)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(0.1f));
                _curTime += 0.1f;

                var levelProgress = _curTime /_level.LevelLength;

                EventBus.Instance.LevelProgressChanged?.Invoke(levelProgress);

                if (_curTime >= _level.LevelLength)
                {
                    EventBus.Instance.LevelTimePassed?.Invoke();
                    _isLevelRunning = false;
                }
            }
        }

        private void OnDestroy()
        {
            _isLevelRunning = false;

            EventBus.Instance.LevelSet -= LevelSet;
            EventBus.Instance.GameStop -= LevelStop;
            EventBus.Instance.GameStart -= GameStart;
        }
    }
}