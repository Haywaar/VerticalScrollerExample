using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs
{
    public class MessageDialog : Dialog
    {
        [SerializeField] private Text _messageText;
        [SerializeField] private Button _okButton;

        public void Init(string text)
        {
            _messageText.text = text;
            _okButton.onClick.AddListener(Hide);
        }
    }
}
