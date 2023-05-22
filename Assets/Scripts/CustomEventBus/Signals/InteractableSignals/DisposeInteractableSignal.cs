using Interactables;

namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал на отключение Interactable
    /// </summary>
    public class DisposeInteractableSignal
    {
        public readonly Interactable Interactable;

        public DisposeInteractableSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}