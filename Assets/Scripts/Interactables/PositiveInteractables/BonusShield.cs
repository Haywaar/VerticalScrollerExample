using UnityEngine;

namespace Examples.VerticalScrollerExample.PositiveInteractable
{
    public class BonusShield : Interactable
    {
        [SerializeField] private float _shieldTime = 4f;
        protected override void Interact()
        {
            EventBus.Instance.AddShield?.Invoke(_shieldTime);
        }
    }
}