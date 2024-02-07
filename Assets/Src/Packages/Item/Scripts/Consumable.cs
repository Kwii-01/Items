using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Items {
    public abstract class Consumable : AItem {
        public abstract override AItem NewInstance();
        public abstract override string Serialize();
        public abstract override AItem Unserialize(string json);

        public abstract void Consume(Item item, int amount);
    }
}