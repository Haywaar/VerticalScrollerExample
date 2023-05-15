using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Окно настроек(звуки, сброс PlayerPrefs и прочее)
/// </summary>
public class SettingsDialog : Window
{
   [SerializeField] private Toggle _soundOn;
   [SerializeField] private Button _resetPlayerPrefsButton;
   [SerializeField] private Button _backButton;

   protected override void Awake()
   {
      base.Awake();
      
      _soundOn.onValueChanged.AddListener(OnSoundOnChanged);
      _resetPlayerPrefsButton.onClick.AddListener(OnResetBtnClicked);
      _backButton.onClick.AddListener(Hide);
   }

   private void OnSoundOnChanged(bool val)
   {
      //TODO
   }

   private void OnResetBtnClicked()
   {
      PlayerPrefs.DeleteAll();
   }
}
