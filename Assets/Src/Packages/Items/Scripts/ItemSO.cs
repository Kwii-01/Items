using System;
using System.Collections;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;

namespace Items {
    [CreateAssetMenu(menuName = "Items/Item", fileName = "Item-")]
    public class ItemSO : ScriptableObject {
        public string Guid = System.Guid.NewGuid().ToString();
        public string Name;
        public Sprite Icon;
        [Min(1)] public int MaxStack = 1;
        [SerializeReference, SubclassSelector] public AItem Instance;

        public Item GetNewItem(int amount = 1, bool withNewInstance = true) => new Item { Stacks = Mathf.Min(amount, this.MaxStack), Data = this, Instance = withNewInstance ? this.Instance.NewInstance() : this.Instance };

        private void Awake() {
            if (string.IsNullOrEmpty(this.Guid) == false) {
                this.Guid = System.Guid.NewGuid().ToString();
            }
        }
    }
}