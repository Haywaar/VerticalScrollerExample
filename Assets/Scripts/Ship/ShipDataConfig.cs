using System.Collections.Generic;
using UnityEngine;

namespace Examples.VerticalScrollerExample.Scripts.Ship
{
    [CreateAssetMenu(fileName = "ShipDataConfig", menuName = "ScriptableObjects/ShipDataConfig", order = 1)]
    public class ShipDataConfig : ScriptableObject
    {
        [SerializeField] private List<ShipData> _shipsData;

        public List<ShipData> ShipsData => _shipsData;
    }
}