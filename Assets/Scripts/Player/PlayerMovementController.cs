using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Player _player;

    private float _minX;
    private float _maxX;

    private bool _canMove = true;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<GameStopSignal>(x => { _canMove = false; });

        _eventBus.Subscribe<GameStartedSignal>(x => { _canMove = true; });

        var spawner = ServiceLocator.Current.Get<InteractablesSpawner>();
        _maxX = spawner.MaxX;
        _minX = spawner.MinX;
    }

    private void Update()
    {
        if (!_canMove)
            return;

        var playerInput = Input.GetAxisRaw("Horizontal");

        if (playerInput == 1.0f && _player.transform.position.x > _maxX)
            return;

        if (playerInput == -1.0f && _player.transform.position.x < _minX)
            return;

        _player.transform.Translate(Vector3.right * (Time.deltaTime * playerInput * _player.SpeedKoef));
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<GameStopSignal>(x => { _canMove = false; });

        _eventBus.Unsubscribe<GameStartedSignal>(x => { _canMove = true; });
    }
}