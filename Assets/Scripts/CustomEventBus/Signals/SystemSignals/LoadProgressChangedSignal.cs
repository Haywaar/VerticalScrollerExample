using UnityEngine;

namespace CustomEventBus.Signals
{
    public class LoadProgressChangedSignal
    {
        public readonly float Progress;

        public LoadProgressChangedSignal(float progress)
        {
            Progress = progress;
        }
    }
}