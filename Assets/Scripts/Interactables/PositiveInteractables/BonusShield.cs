using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    public class BonusShield : Interactable
    {
        [SerializeField] private float _shieldTime = 4f;
        protected override void Interact()
        {
            _eventBus.Invoke(new AddShieldSignal(_shieldTime));
        }
    }
}