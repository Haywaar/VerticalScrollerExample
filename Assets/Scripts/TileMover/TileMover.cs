using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

/// <summary>
/// Логика передвижения заднего фона
/// </summary>
public class TileMover : MonoBehaviour
{
    [SerializeField] private List<TileData> _tiles;
    
    private InteractableMover _interactableMover;
    private float _speedKoef;

    private bool _isRunning;

    private EventBus _eventBus;
    public void Init()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<GameStartedSignal>(OnGameStart);
        _eventBus.Subscribe<GameStopSignal>(OnGameStop);
        _interactableMover = ServiceLocator.Current.Get<InteractableMover>();
    }

    private void OnGameStart(GameStartedSignal signal)
    {
        _isRunning = true;
    }
    
    private void OnGameStop(GameStopSignal signal)
    {
        _isRunning = false;
    }

    private void Update()
    {
        if(!_isRunning)
            return;
        
        _speedKoef = _interactableMover.SpeedKoef;
        foreach (var tile in _tiles)
        {
           tile.Move(_speedKoef);
        }
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<GameStartedSignal>(OnGameStart);
        _eventBus.Unsubscribe<GameStopSignal>(OnGameStop);
    }
}
