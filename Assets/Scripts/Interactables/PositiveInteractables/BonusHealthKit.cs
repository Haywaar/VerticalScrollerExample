using Examples.VerticalScrollerExample;
using UnityEngine;

public class BonusHealthKit : Interactable
{
    [SerializeField] private int _healValue = 1;
    protected override void Interact()
    {
        EventBus.Instance.AddHealth?.Invoke(_healValue);
    }
}
