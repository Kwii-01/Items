using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Items {
    public class InventoryUI : MonoBehaviour {
        [SerializeField] private Inventory _inventory;
        [SerializeField] private Transform _content;

        private Dictionary<Item, ItemUI> _itemUIs;

        private void Start() {
            this.LoadInventory();
            this._inventory.OnItemAdded += this.OnItemAddedToInventory;
            this._inventory.OnItemRemoved += this.OnItemRemovedFromInventory;
        }

        private void OnItemAddedToInventory(Item item, int amount) {
            if (this._itemUIs.ContainsKey(item)) {
                this._itemUIs[item].Set(item);
            } else {
                this._itemUIs[item] = ItemsManager.Instance.CreateItemUI(item, this._content);
            }
            this._itemUIs[item].SetNotif(true);
        }

        private void OnItemRemovedFromInventory(Item item, int amount) {
            if (item.Stacks <= 0) {
                Destroy(this._itemUIs[item]);
                this._itemUIs.Remove(item);
            } else {
                this._itemUIs[item].Set(item);
                this._itemUIs[item].SetNotif(true);
            }
        }

        private void LoadInventory() {
            this._itemUIs = new Dictionary<Item, ItemUI>();
            foreach (Item item in this._inventory.Items) {
                this._itemUIs[item] = ItemsManager.Instance.CreateItemUI(item, this._content);
            }
        }
    }
}