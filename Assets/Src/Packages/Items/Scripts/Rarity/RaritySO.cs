using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Items {
    [CreateAssetMenu(menuName = "Items/Rarity")]
    public class RaritySO : ScriptableObject, IComparable<RaritySO> {
        public int Order;
        public string Name;
        public Color PrimaryColor;
        public Color SecondaryColor;

        public int CompareTo(RaritySO other) {
            return this.Order.CompareTo(other.Order);
        }
    }
}
