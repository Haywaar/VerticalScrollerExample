using System.Collections;
using CustomEventBus;
using CustomEventBus.Signals;
using Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader;
using UnityEngine;

public class Player : MonoBehaviour, IService
{
    [SerializeField] private int _health = 3;
    [SerializeField] private float _speedKoef = 3f;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private GameObject _shieldObject;

    private int _score;

    public int Health => _health;
    public float SpeedKoef => _speedKoef;

    private bool _isShielded = false;
    public int Score => _score;

    private EventBus _eventBus;

    private void Start()
    {
        _eventBus = ServiceLocator.Current.Get<EventBus>();
        _eventBus.Subscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Subscribe<AddHealthSignal>(OnAddHealth);
        _eventBus.Subscribe<AddScoreSignal>(OnScoreAdded);
        _eventBus.Subscribe<AddShieldSignal>(AddShield);
        _eventBus.Subscribe<GameStartedSignal>(GameStarted);
        _eventBus.Subscribe<GameStopSignal>(GameStop);

        var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
        var shipData = shipDataLoader.GetCurrentShipData();
        _spriteRenderer.sprite = shipData.ShipSprite;
        _speedKoef = shipData.MovementSpeed;
    }

    private void OnScoreAdded(AddScoreSignal signal)
    {
        _score += signal.Value;
        _eventBus.Invoke(new ScoreChangedSignal(_score));
    }

    private void OnScoreAdded(int value)
    {
        _score += value;
        _eventBus.Invoke(new ScoreChangedSignal(_score));
    }

    private void GameStarted(GameStartedSignal signal)
    {
        //TODO - хелс должен лежать в конфиге уровня
        _health = 3;
        _eventBus.Invoke(new HealthChangedSignal(_health));


        _score = 0;
        _eventBus.Invoke(new ScoreChangedSignal(_score));
    }

    private void OnPlayerDamaged(PlayerDamagedSignal signal)
    {
        int val = signal.Health;

        if (_isShielded)
            return;

        _health -= val;
        if (_health < 0)
        {
            _health = 0;
        }

        _eventBus.Invoke(new HealthChangedSignal(_health));

        if (_health == 0)
        {
            _eventBus.Invoke(new PlayerDeadSignal());
        }
    }

    private void OnAddHealth(AddHealthSignal signal)
    {
        _health += signal.Value;

        //TODO - в настройки
        if (_health > 3)
        {
            OnScoreAdded(50 * (_health - 3));
            _health = 3;
        }

        _eventBus.Invoke(new HealthChangedSignal(_health));
    }

    private void GameStop(GameStopSignal signal)
    {
        _shieldObject.gameObject.SetActive(false);
        _isShielded = false;
    }

    private void AddShield(AddShieldSignal signal)
    {
        StartCoroutine(ActivateShield(signal.ShieldDuration));
    }

    private IEnumerator ActivateShield(float waitTime)
    {
        _isShielded = true;
        _shieldObject.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitTime);
        _shieldObject.gameObject.SetActive(false);
        _isShielded = false;
    }

    private void OnDestroy()
    {
        _eventBus.Unsubscribe<PlayerDamagedSignal>(OnPlayerDamaged);
        _eventBus.Unsubscribe<AddHealthSignal>(OnAddHealth);

        _eventBus.Unsubscribe<AddScoreSignal>(OnScoreAdded);
        _eventBus.Unsubscribe<AddShieldSignal>(AddShield);

        _eventBus.Unsubscribe<GameStartedSignal>(GameStarted);
        _eventBus.Unsubscribe<GameStopSignal>(GameStop);
    }
}