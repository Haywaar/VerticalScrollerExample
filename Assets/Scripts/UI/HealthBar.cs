﻿using System.Collections.Generic;
using ConfigLoader.Ship;
using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Полоска здоровья игрока(кол-во жизней)
    /// Спрайт сердечка аналогичен спрайту самолётика игрока
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private List<Image> _hearts;

        private EventBus _eventBus;

        public void Init()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<HealthChangedSignal>(DisplayHealth);
            _eventBus.Subscribe<AllDataLoadedSignal>(OnAllDataLoaded);
        }

        private void OnAllDataLoaded(AllDataLoadedSignal signal)
        {
            var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
            var curShipData = shipDataLoader.GetCurrentShipData();
            var sprite = curShipData.ShipSprite;
            foreach (var heartImage in _hearts)
            {
                heartImage.sprite = sprite;
            }
        }

        private void DisplayHealth(HealthChangedSignal signal)
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                bool isHeartActive = i <= (signal.Health - 1);
                _hearts[i].gameObject.SetActive(isHeartActive);
            }
        }

        private void OnDestroy()
        {
            _eventBus.Unsubscribe<HealthChangedSignal>(DisplayHealth);
        }
    }
}