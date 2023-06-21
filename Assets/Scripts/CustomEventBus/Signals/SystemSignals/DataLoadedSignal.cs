namespace CustomEventBus.Signals
{
    public class DataLoadedSignal
    {
        public readonly ILoader Loader;
        public DataLoadedSignal(ILoader loader)
        {
            Loader = loader;
        }
    }
}