using System;
using UnityEngine;

namespace Examples.VerticalScrollerExample.Scripts.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private Player _player;

        private float _minX;
        private float _maxX;

        private bool _canMove = true;

        private void Awake()
        {
            EventBus.Instance.GameStop += () =>
            {
                _canMove = false;
            };
            
            EventBus.Instance.GameStart += () =>
            {
                _canMove = true;
            };
        }

        private void Start()
        {
            var spawner = ServiceLocator.Current.Get<InteractablesSpawner>();
            _maxX = spawner.MaxX;
            _minX = spawner.MinX;
        }

        private void Update()
        {
            if(!_canMove)
                return;
            
            var playerInput = Input.GetAxisRaw("Horizontal");
            
            if(playerInput == 1.0f && _player.transform.position.x > _maxX)
                return;
            
            if(playerInput == -1.0f && _player.transform.position.x < _minX)
                return;
            
            _player.transform.Translate(Vector3.right * (Time.deltaTime * playerInput * _player.SpeedKoef));
        }

        private void OnDestroy()
        {
            EventBus.Instance.GameStop -= () => { _canMove = false; };
            EventBus.Instance.GameStart -= () => { _canMove = true; };
        }
    }
}