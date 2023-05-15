using System.Linq;
using DefaultNamespace;
using Examples.VerticalScrollerExample;
using Examples.VerticalScrollerExample.Scripts.Ship.ShipDataLoader;
using Examples.VerticalScrollerExample.Scripts.UI;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Окно выбора самолётика
/// </summary>
public class CustomizeShipDialog : Window
{
    [SerializeField] private GridLayoutGroup _elementsGrid;
    [SerializeField] private CustomizeShipSlot _shipSlotPrefab;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Text _goldValue;
    
    protected override void Awake()
    {
        base.Awake();
        
        _exitButton.onClick.AddListener(Hide);
        InitShipSlots();

        var gold = ServiceLocator.Current.Get<GoldController>().Gold;
        _goldValue.text = "Gold: " + gold;

        EventBus.Instance.GoldChanged += RedrawGold;
    }

    private void InitShipSlots()
    {
        var shipDataLoader = ServiceLocator.Current.Get<IShipDataLoader>();
        var shipsData = shipDataLoader.GetShipsData();
        shipsData = shipsData.OrderBy(x => x.ID);

        foreach (var shipData in shipsData)
        {
            var shipSlot = GameObject.Instantiate(_shipSlotPrefab, _elementsGrid.transform);
            bool purchased = PlayerPrefs.GetInt(StringConstants.SHIP_PURCHASED + shipData.ID, 0) == 1 || shipData.PurchasePrice == 0;
            shipSlot.Init(shipData.ID, shipData.ShipSprite, shipData.PurchasePrice, purchased);
        }
    }
    
    private void  RedrawGold(int gold)
    {
        _goldValue.text = "Gold: " + gold;
    }

    private void OnDestroy()
    {
        EventBus.Instance.GoldChanged -= RedrawGold;
    }
}