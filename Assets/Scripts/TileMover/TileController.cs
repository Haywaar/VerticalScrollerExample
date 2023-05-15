using System;
using System.Collections;
using System.Collections.Generic;
using Examples.VerticalScrollerExample;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private List<TileData> _tiles;
    
    private MovementController _movementController;
    private float _speedKoef;

    private bool _isRunning;
    private void Start()
    {
        _movementController = ServiceLocator.Current.Get<MovementController>();
        
        EventBus.Instance.GameStart += OnGameStart;
        EventBus.Instance.GameStop += OnGameStop;
    }

    private void OnGameStart()
    {
        _isRunning = true;
    }
    
    private void OnGameStop()
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
        EventBus.Instance.GameStart -= OnGameStart;
        EventBus.Instance.GameStop -= OnGameStop;
    }
}
