using Newtonsoft.Json;
using UnityEngine;

namespace ConfigLoader.Ship.JSON
{
    public struct JsonShipData
    {
        [JsonProperty] [SerializeField] private int id;
        [JsonProperty] [SerializeField] private float movementSpeed;
        [JsonProperty] [SerializeField] private int purchasePrice;
        [JsonProperty] [SerializeField] private string shipSprite;

        public int ID => id;
        public float MovementSpeed => movementSpeed;
        public int PurchasePrice => purchasePrice;
        public string ShipSprite => shipSprite;
    }
}