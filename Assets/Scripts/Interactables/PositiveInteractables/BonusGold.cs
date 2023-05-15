using UnityEngine;

namespace Examples.VerticalScrollerExample.PositiveInteractable
{
    public class BonusGold : Interactable
    {
        protected override void Interact()
        {
            EventBus.Instance.AddScore?.Invoke(10);
        }
    }
}