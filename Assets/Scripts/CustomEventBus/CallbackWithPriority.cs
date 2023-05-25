
namespace CustomEventBus
{
    public class CallbackWithPriority
    {
        /// <summary>
        /// Чем выше Priority, тем раньше вызовется ивент
        /// </summary>
        public readonly int Priority;
        public readonly object Callback;

        public CallbackWithPriority(int priority, object callback)
        {
            Priority = priority;
            Callback = callback;
        }
    }
}