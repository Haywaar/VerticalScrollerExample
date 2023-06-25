using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs
{
    /// <summary>
    /// Окошко "Идёт загрузка"
    /// при поступлении сигнала AllResourcesLoaded исчезает
    /// </summary>
    public class LoadingDialog : Dialog
    {
        [SerializeField] private Text _progressText;
        
        private EventBus _eventBus;
        
        private void Start()
        {
            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<LoadProgressChangedSignal>(LoadProgressChanged);
            _eventBus.Subscribe<AllDataLoadedSignal>(OnAllResourcesLoaded);
            _progressText.text = "LoadingProgress is " + 0 + "%";
        }

        private void LoadProgressChanged(LoadProgressChangedSignal signal)
        {
            RedrawProgress(signal.Progress);
        }

        private void OnAllResourcesLoaded(AllDataLoadedSignal signal)
        {
            Hide();
        }

        private void RedrawProgress(float progress)
        {
            progress *= 100;
            _progressText.text = "LoadingProgress is " + progress + "%";;
        }
    }
}