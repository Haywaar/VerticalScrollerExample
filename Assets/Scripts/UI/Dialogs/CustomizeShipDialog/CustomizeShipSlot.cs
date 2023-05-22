using System;
using CustomEventBus;
using CustomEventBus.Signals;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Dialogs
{
    /// <summary>
    /// Слот выбора самолётика(лежит в CustomizeShipDialog)
    /// </summary>
    public class CustomizeShipSlot : MonoBehaviour
    {
        [SerializeField] private Image _shipImage;
        [SerializeField] private Button _selectShipButton;
        [SerializeField] private Text _slotPrice;
        
        [SerializeField] private GameObject _selectedCheck;
        [SerializeField] private GameObject _notPurchasedLock;
        
        private int _price;
        private bool _isPurchased;
        private int _shipId;

        private EventBus _eventBus;

        private void Start()
        {
            _selectShipButton.onClick.AddListener(TrySelectShip);

            _eventBus = ServiceLocator.Current.Get<EventBus>();
            _eventBus.Subscribe<SelectShipSignal>(OnShipSelected);
        }
        
        public void Init(int id, Sprite shipSprite, int price, bool isPurchased)
        {
            _shipId = id;
            _shipImage.sprite = shipSprite;
            _price = price;
            _slotPrice.text = price.ToString();
            _isPurchased = isPurchased;
            
            var selectedShipId = PlayerPrefs.GetInt(StringConstants.SELECTED_SHIP, 0);
            _selectedCheck.SetActive(selectedShipId == _shipId);
            _notPurchasedLock.SetActive(!isPurchased);
        }

        private void OnShipSelected(SelectShipSignal signal)
        {
            _notPurchasedLock.SetActive(!_isPurchased);
            _selectedCheck.SetActive(signal.ShipId == _shipId);
        }

        private void TrySelectShip()
        {
            if (!_isPurchased)
            {
                var goldController = ServiceLocator.Current.Get<GoldController>();

                bool canBuy = goldController.HaveEnoughGold(_price);

                if (canBuy)
                {
                    var dialog = WindowManager.GetWindow<PurchaseItemDialog>();
                    dialog.Init("Are you sure, that you want to buy this ship for " + _price + " gold ?", () =>
                    {
                        _eventBus.Invoke(new SpendGoldSignal(_price));
                        
                        _isPurchased = true;
                        PlayerPrefs.SetInt(StringConstants.SHIP_PURCHASED + _shipId, 1);
                        
                        _eventBus.Invoke(new SelectShipSignal(_shipId));
                        PlayerPrefs.SetInt(StringConstants.SELECTED_SHIP, _shipId);
                    });
                }
                else
                {
                    var messageDialog = WindowManager.GetWindow<MessageDialog>();
                    var messageStr = String.Format("Not enough gold! You have {0} gold, but you need {1} gold!", goldController.Gold, _price);
                    messageDialog.Init(messageStr);
                }
            }
            else
            {
                _eventBus.Invoke(new SelectShipSignal(_shipId));
                PlayerPrefs.SetInt(StringConstants.SELECTED_SHIP, _shipId);
            }
        }

        private void OnDestroy()
        {
            _selectShipButton.onClick.RemoveListener(TrySelectShip);
            _eventBus.Unsubscribe<SelectShipSignal>(OnShipSelected);
        }
    }
}