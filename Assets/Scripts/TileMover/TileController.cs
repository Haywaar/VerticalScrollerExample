using System.Collections.Generic;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private List<TileData> _tiles;
    
    private MovementController _movementController;
    private float _speedKoef;

    private bool _isRunning;

    private EventBus _eventBus;
    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        
        _eventBus.Subscribe<GameStartedSignal>(OnGameStart);
        _eventBus.Subscribe<GameStopSignal>(OnGameStop);
        
        _movementController = ServiceLocator.Current.Get<MovementController>();
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
        
        _speedKoef = _movementController.SpeedKoef;
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
