using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Items {
    public abstract class Consumable : AItem {
        public abstract void Consume(Item item, int amount);
    }
}