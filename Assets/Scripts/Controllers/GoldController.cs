using CustomEventBus;
using CustomEventBus.Signals;
using DefaultNamespace;
using UnityEngine;

public class GoldController : IService
{
    private int _gold;
    public int Gold => _gold;

    private EventBus _eventBus;

    public GoldController()
    {
        _gold = PlayerPrefs.GetInt(StringConstants.GOLD, 7);

        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<AddGoldSignal>(OnAddGold);
        _eventBus.Subscribe<SpendGoldSignal>(SpendGold);
        _eventBus.Subscribe<GoldChangedSignal>(GoldChanged);
    }

    private void OnAddGold(AddGoldSignal signal)
    {
        _gold += signal.Value;
        _eventBus.Invoke(new GoldChangedSignal(_gold));
    }

    public bool HaveEnoughGold(int gold)
    {
        return _gold >= gold;
    }

    private void SpendGold(SpendGoldSignal signal)
    {
        if (HaveEnoughGold(signal.Value))
        {
            _gold -= signal.Value;
            _eventBus.Invoke(new GoldChangedSignal(_gold));
        }
    }

    private void GoldChanged(GoldChangedSignal signal)
    {
        PlayerPrefs.SetInt(StringConstants.GOLD, signal.Gold);
    }
}