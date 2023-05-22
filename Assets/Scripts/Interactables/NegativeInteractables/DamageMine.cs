using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    public class DamageMine : Interactable
    {
        [SerializeField] private int _damageValue = 1;
        protected override void Interact()
        {
            _eventBus.Invoke(new PlayerDamagedSignal(_damageValue));
        }
    }
}