using DefaultNamespace;
using UnityEngine;

namespace Examples.VerticalScrollerExample
{
    public class GoldController : IService
    {
        private int _gold;
        public int Gold => _gold;

        public GoldController()
        {
            _gold = PlayerPrefs.GetInt(StringConstants.GOLD, 7);

            EventBus.Instance.AddGold += OnAddGold;
            EventBus.Instance.SpendGold += SpendGold;
            EventBus.Instance.GoldChanged += GoldChanged;
        }

        private void OnAddGold(int gold)
        {
            _gold += gold;
            EventBus.Instance.GoldChanged?.Invoke(_gold);
        }

        public bool HaveEnoughGold(int gold)
        {
            return _gold >= gold;
        }

        private void SpendGold(int gold)
        {
            if (HaveEnoughGold(gold))
            {
                _gold -= gold;
                EventBus.Instance.GoldChanged?.Invoke(_gold);
            }
        }
        
        private void GoldChanged(int gold)
        {
            PlayerPrefs.SetInt(StringConstants.GOLD, gold);
        }
    }
}