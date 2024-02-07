using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Items {
    public class InventoryUI : MonoBehaviour {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _content;

        private void Start() {
            this._inventory.OnItemAdded += this.OnItemAddedToInventory;
            this._inventory.OnItemRemoved += this.OnItemRemovedFromInventory;
        }

        private void OnItemAddedToInventory(Item item, int amount) {

        }

        private void OnItemRemovedFromInventory(Item item, int amount) {

        }
    }
}