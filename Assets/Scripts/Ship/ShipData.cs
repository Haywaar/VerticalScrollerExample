using UnityEngine;

namespace Examples.VerticalScrollerExample.Scripts.Ship
{
    [System.Serializable]
    public struct ShipData
    {
        [SerializeField] private int _id;

        [SerializeField] private float _movementSpeed;

        [SerializeField] private int _purchasePrice;

        [SerializeField] private Sprite _shipSprite;

        public int ID => _id;

        public float MovementSpeed => _movementSpeed;

        public int PurchasePrice => _purchasePrice;

        public Sprite ShipSprite => _shipSprite;
    }
}