using Interactables;

namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том, что Interactable активировался и появился на поле
    /// </summary>
    public class InteractableActivatedSignal
    {
        public readonly Interactable Interactable;

        public InteractableActivatedSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}