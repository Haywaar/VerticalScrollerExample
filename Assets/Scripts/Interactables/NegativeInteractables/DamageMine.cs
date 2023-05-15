using UnityEngine;

namespace Examples.VerticalScrollerExample.NegativeInteractables
{
    public class DamageMine : Interactable
    {
        [SerializeField] private int _damageValue = 1;
        protected override void Interact()
        {
            EventBus.Instance.PlayerDamaged.Invoke(_damageValue);
        }
    }
}