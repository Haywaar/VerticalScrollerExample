using CustomEventBus;
using CustomEventBus.Signals;
using UnityEngine;

/// <summary>
/// Система отвечающая за золото:
/// Начисление, трата, изменение золота
/// </summary>
public class GoldController : IService, IDisposable
{
    private int _gold;
    public int Gold => _gold;

    private EventBus _eventBus;

    public void Init()
    {
        _gold = PlayerPrefs.GetInt(StringConstants.GOLD, 7);

        _eventBus = ServiceLocator.Current.Get<EventBus>();

        _eventBus.Subscribe<AddGoldSignal>(OnAddGold);
        _eventBus.Subscribe<SpendGoldSignal>(SpendGold);
        _eventBus.Subscribe<GoldChangedSignal>(GoldChanged);
        _eventBus.Subscribe<LevelFinishedSignal>(LevelFinished);
    }

    private void OnAddGold(AddGoldSignal signal)
    {
        OnAddGold(signal.Value);
    }
    
    private void OnAddGold(int value)
    {
        _gold += value;
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

    private void LevelFinished(LevelFinishedSignal signal)
    {
        OnAddGold(signal.LevelData.GoldForPass);
    }

    public void Dispose()
    {
        _eventBus.Unsubscribe<AddGoldSignal>(OnAddGold);
        _eventBus.Unsubscribe<SpendGoldSignal>(SpendGold);
        _eventBus.Unsubscribe<GoldChangedSignal>(GoldChanged);
        _eventBus.Unsubscribe<LevelFinishedSignal>(LevelFinished);
    }
}