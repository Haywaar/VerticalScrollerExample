using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialog : Window
{
    [SerializeField] private Text _messageText;
    [SerializeField] private Button _okButton;

    public void Init(string text)
    {
        _messageText.text = text;
        _okButton.onClick.AddListener(Hide);
    }
}
