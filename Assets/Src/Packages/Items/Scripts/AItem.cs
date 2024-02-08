using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Items {
    public abstract class AItem {
        public abstract AItem NewInstance();
        public abstract AItem Unserialize(string json);
        public abstract string Serialize();
    }

    [Serializable]
    public class Item {
        public int Stacks;
        public ItemSO Data;
        public AItem Instance;


        [Serializable]
        private class Serialized {
            public string Guid;
            public int Stacks;
            public string Data;
        }

        public string Serialize() {
            if (this.Data == null) {
                return string.Empty;
            }
            return JsonUtility.ToJson(new Serialized { Stacks = this.Stacks, Data = this.Instance.Serialize(), Guid = this.Data.Guid });
        }

        public static Item Unserialize(string json) {
            if (string.IsNullOrEmpty(json)) {
                return Empty;
            }
            Serialized serializedData = JsonUtility.FromJson<Serialized>(json);
            ItemSO itemData = ItemsManager.Instance.Get(serializedData.Guid);
            return new Item { Stacks = serializedData.Stacks, Data = itemData, Instance = itemData.Instance.Unserialize(serializedData.Data) };
        }

        public static Item Empty = new Item { Stacks = 1, Data = null, Instance = null };
    }
}