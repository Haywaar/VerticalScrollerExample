using System.Collections;
using Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader;
using UnityEngine;

namespace Examples.VerticalScrollerExample.Scripts.Player
{
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
        private void Awake()
        {
            EventBus.Instance.PlayerDamaged += OnPlayerDamaged;
            EventBus.Instance.AddHealth += OnAddHealth;
            EventBus.Instance.AddScore += OnScoreAdded;
            EventBus.Instance.AddShield += AddShield;

            EventBus.Instance.GameStart += GameStarted;
            EventBus.Instance.GameStop += GameStop;
        }

        private void Start()
        {
            var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
            var shipData = shipDataLoader.GetCurrentShipData();
            _spriteRenderer.sprite = shipData.ShipSprite;
            _speedKoef = shipData.MovementSpeed;
        }

        private void OnScoreAdded(int val)
        {
            _score += val;
            EventBus.Instance.ScoreChanged?.Invoke(_score);
        }

        private void GameStarted()
        {
            //TODO - хелс должен лежать в конфиге уровня
            _health = 3;
            EventBus.Instance.PlayerHealthChanged?.Invoke(_health);
            
            
            _score = 0;
            EventBus.Instance.ScoreChanged?.Invoke(_score);
        }

        private void OnPlayerDamaged(int val)
        {
            if(_isShielded)
                return;
            
            _health -= val;
            if (_health < 0)
            {
                _health = 0;
            }

            EventBus.Instance.PlayerHealthChanged?.Invoke(_health);

            if (_health == 0)
            {
                EventBus.Instance.PlayerDead?.Invoke();
            }
        }
        
        private void OnAddHealth(int val)
        {
            _health += val;
            
            //TODO - в настройки
            if (_health > 3)
            {
                OnScoreAdded(50*(_health - 3));
                _health = 3;
            }
            
            EventBus.Instance.PlayerHealthChanged?.Invoke(_health);
        }
        
        private void GameStop()
        {
            _shieldObject.gameObject.SetActive(false);
            _isShielded = false;
        }

        private void AddShield(float time)
        {
            StartCoroutine(ActivateShield(time));
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
            EventBus.Instance.PlayerDamaged -= OnPlayerDamaged;
            EventBus.Instance.AddHealth -= OnAddHealth;
            EventBus.Instance.AddScore -= OnScoreAdded;
            EventBus.Instance.GameStart -= GameStarted;
            
            EventBus.Instance.AddShield -= AddShield;
            EventBus.Instance.GameStop -= GameStop;
        }
    }
}