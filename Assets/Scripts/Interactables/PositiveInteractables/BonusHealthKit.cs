using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    public class BonusHealthKit : Interactable
    {
        [SerializeField] private int _healValue = 1;
        protected override void Interact()
        {
            _eventBus.Invoke(new AddHealthSignal(_healValue));
        }
    }
}
