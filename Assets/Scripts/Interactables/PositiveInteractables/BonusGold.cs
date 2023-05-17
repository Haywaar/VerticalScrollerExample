using UnityEngine;

namespace Examples.VerticalScrollerExample.PositiveInteractable
{
    public class BonusGold : Interactable
    {
        [SerializeField] private int _goldValue = 10;
        protected override void Interact()
        {
            EventBus.Instance.AddScore?.Invoke(_goldValue);
        }
    }
}