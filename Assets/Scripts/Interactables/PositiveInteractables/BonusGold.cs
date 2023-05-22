using CustomEventBus.Signals;
using UnityEngine;

namespace Interactables
{
    public class BonusGold : Interactable
    {
        [SerializeField] private int _goldValue = 10;
        protected override void Interact()
        {
            _eventBus.Invoke(new AddScoreSignal(_goldValue));
        }
    }
}