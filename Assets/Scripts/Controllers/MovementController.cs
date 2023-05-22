using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using Interactables;
using UnityEngine;

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

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<InteractableActivatedSignal>(TryAdd);
        _eventBus.Subscribe<InteractableDisposedSignal>(TryRemove);
        _eventBus.Subscribe<SetLevelSignal>(OnLevelSet);

        _eventBus.Subscribe<GameStartedSignal>(StartLevel);
        _eventBus.Subscribe<GameStopSignal>(StopLevel);
    }

    private void TryAdd(InteractableActivatedSignal signal)
    {
        if (!_interactables.Contains(signal.Interactable) && _isLevelRunning)
        {
            _interactables.Add(signal.Interactable);
        }
    }

    private void TryRemove(InteractableDisposedSignal signal)
    {
        if (_interactables.Contains(signal.Interactable))
        {
            _interactables.Remove(signal.Interactable);
        }
    }

    private void OnLevelSet(SetLevelSignal signal)
    {
        var level = signal.Level;

        _startSpeed = level.StartSpeed;
        _endSpeed = level.EndSpeed;
        _levelDuration = level.LevelLength;
    }

    private void StartLevel(GameStartedSignal signal)
    {
        _isLevelRunning = true;
        _timePassed = 0f;
    }

    private void StopLevel(GameStopSignal signal)
    {
        _isLevelRunning = false;
    }

    private void Update()
    {
        if (!_isLevelRunning)
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
                _eventBus.Invoke(new DisposeInteractableSignal(_interactables[i]));
            }
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<InteractableActivatedSignal>(TryAdd);
        _eventBus.Unsubscribe<InteractableDisposedSignal>(TryRemove);
        _eventBus.Unsubscribe<SetLevelSignal>(OnLevelSet);
        _eventBus.Unsubscribe<GameStartedSignal>(StartLevel);
        _eventBus.Unsubscribe<GameStopSignal>(StopLevel);
    }
}