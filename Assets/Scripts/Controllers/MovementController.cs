using System;
using System.Collections.Generic;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    /// <summary>
    /// Передвигает все Interactable на экране
    /// Выключает их если объекты вышли за пределы экрана
    /// </summary>
    public class MovementController : MonoBehaviour, IService
    {
        [SerializeField] private float _speedKoef;

        public float SpeedKoef => _speedKoef;

        [SerializeField] private const float _lowBorderY = -5f;
        private readonly List<Interactable> _interactables = new List<Interactable>();

        private bool _isLevelRunning;

        private float _startSpeed;
        private float _endSpeed;
        private float _levelDuration;
        private float _timePassed;

        private void Awake()
        {
            Init();

            EventBus.Instance.InteractableActivated += TryAdd;
            EventBus.Instance.InteractableDisposed += TryRemove;

            EventBus.Instance.LevelSet += OnLevelSet;
            EventBus.Instance.GameStop += StopLevel;
            EventBus.Instance.GameStart += StartLevel;
        }

        private void Init()
        {
        }

        private void TryAdd(Interactable obj)
        {
            if (!_interactables.Contains(obj) && _isLevelRunning)
            {
                _interactables.Add(obj);
            }
        }
        
        private void TryRemove(Interactable obj)
        {
            if (_interactables.Contains(obj))
            {
                _interactables.Remove(obj);
            }
        }

        private void OnLevelSet(Level level)
        {
            _startSpeed = level.StartSpeed;
            _endSpeed = level.EndSpeed;
            _levelDuration = level.LevelLength;
        }
        
        private void StartLevel()
        {
            _isLevelRunning = true;
            _timePassed = 0f;
        }
        
        private void StopLevel()
        {
            _isLevelRunning = false;
        }

        private void Update()
        {
            if(!_isLevelRunning)
                return;
            
            foreach (var interactable in _interactables)
            {
                interactable.transform.Translate(Vector3.down * (Time.deltaTime * _speedKoef));
            }

            _timePassed += Time.deltaTime;
            _speedKoef = Mathf.Lerp(_startSpeed, _endSpeed, (_timePassed / _levelDuration));
        }

        private void LateUpdate()
        {
            if (_interactables.Count == 0)
                return;

            for (int i = 0; i < _interactables.Count; i++)
            {
                if (_interactables[i].transform.position.y <= _lowBorderY || !_isLevelRunning)
                {
                    EventBus.Instance.DisposeInteractable(_interactables[i]);
                }
            }
        }
        
        private void OnDestroy()
        {
            EventBus.Instance.InteractableActivated -= TryAdd;
            EventBus.Instance.InteractableDisposed -= TryRemove;
            
            EventBus.Instance.LevelSet -= OnLevelSet;
            EventBus.Instance.GameStop -= StopLevel;
            EventBus.Instance.GameStart -= StartLevel;
        }
    }
}