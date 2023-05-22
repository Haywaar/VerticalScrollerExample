using Interactables;

namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал о том, что Interactable отключён
    /// </summary>
    public class InteractableDisposedSignal
    {
        public readonly Interactable Interactable;

        public InteractableDisposedSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}