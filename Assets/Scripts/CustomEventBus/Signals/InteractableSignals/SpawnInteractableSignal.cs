using Interactables;

namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал-команда - "Заспавни Interactable"
    /// </summary>
    public class SpawnInteractableSignal
    {
        public readonly Interactable Interactable;

        public SpawnInteractableSignal(Interactable interactable)
        {
            Interactable = interactable;
        }
    }
}