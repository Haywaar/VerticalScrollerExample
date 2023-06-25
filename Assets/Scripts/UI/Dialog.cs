using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Dialog : MonoBehaviour
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