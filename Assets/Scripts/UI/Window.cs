using UnityEngine;
using UnityEngine.UI;

namespace Examples.VerticalScrollerExample.Scripts.UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] private Button _outsideClickArea;

        protected virtual void Awake()
        {
            if (_outsideClickArea != null)
            {
                _outsideClickArea.onClick.AddListener(Hide);
            }
        }

        protected void Hide()
        {
            Destroy(gameObject);
        }

        protected void OnDestroy()
        {
            if (_outsideClickArea != null)
            {
                _outsideClickArea.onClick.RemoveAllListeners();
            }
        }
    }
}