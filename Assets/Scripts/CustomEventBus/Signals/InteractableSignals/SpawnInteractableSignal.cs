using Interactables;

namespace CustomEventBus.Signals
{
    /// <summary>
    /// Сигнал-команда - "Заспавни Interactable"
    /// </summary>
    public class SpawnInteractableSignal
    {
        public readonly InteractableType InteractableType;
        public readonly int Grade;

        public SpawnInteractableSignal(InteractableType type, int grade)
        {
            InteractableType = type;
            Grade = grade;
        }
    }
}