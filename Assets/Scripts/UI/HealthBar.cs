using System;
using System.Collections.Generic;
using Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Examples.VerticalScrollerExample.Scripts.UI
{
    /// <summary>
    /// Полоска здоровья игрока(кол-во жизней)
    /// Спрайт сердечка аналогичен спрайту самолётика игрока
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private List<Image> _hearts;
        private void Awake()
        {
            EventBus.Instance.PlayerHealthChanged += DisplayHealth;
        }

        private void Start()
        {
            var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
            var curShipData = shipDataLoader.GetCurrentShipData();
            var sprite = curShipData.ShipSprite;
            foreach (var heartImage in _hearts)
            {
                heartImage.sprite = sprite;
            }
        }

        private void DisplayHealth(int health)
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                bool isHeartActive = i <= (health - 1);
                _hearts[i].gameObject.SetActive(isHeartActive);
            }
        }
        
        private void OnDestroy()
        {
            EventBus.Instance.PlayerHealthChanged -= DisplayHealth;
        }
    }
}