using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using SavingSystem;

namespace Items {
    public class Inventory : MonoBehaviour {
        [SerializeField] private string _saveKey;
        [SerializeField] private ASaver _savingSystem;
        [SerializeField] private List<Item> _items = new List<Item>();

        public event Action<Item, int> OnItemAdded;
        public event Action<Item, int> OnItemRemoved;

        private void Start() {
            if (this._savingSystem.HasKey(this._saveKey) == false) {
                this._items = this._savingSystem.LoadManyJson<string>(this._saveKey).Select(itemJson => Item.Unserialize(itemJson)).ToList();
            }
        }

        public void Reset() {
            this._saveKey = Guid.NewGuid().ToString();
        }

        private void OnDestroy() {
            this.Save();
        }

        public string[] Serialize() {
            return this._items.Select(item => item.Serialize()).ToArray();
        }


        //? GETTERS

        public IEnumerable<Item> GetItemsOfType(ItemSO itemSO) {
            return this._items.Where(item => item.Data == itemSO);
        }

        public IEnumerable<Item> GetItemsOfType<T>() where T : AItem {
            return this._items.Where(item => item.Instance is T);
        }

        public Item Get(int index) {
            if (index > this._items.Count) {
                return Item.Empty;
            }
            return this._items[index];
        }

        //? UTILITY

        public void Swap(int indA, int indB) {
            Item temp = this._items[indA];
            this._items[indA] = this._items[indB];
            this._items[indB] = temp;
        }

        public void Save() {
            this._savingSystem.SaveJson(this._saveKey, this._items.Select(item => item.Serialize()).ToArray());
        }


        //? ADDS

        public void Add(Item item) {
            int stacks = item.Stacks;
            int stackAdded;
            List<Item> otherItems = this.GetItemsOfType(item.Data).Where(item => item.Stacks < item.Data.MaxStack).ToList();
            foreach (Item otherItem in otherItems) {
                stackAdded = Mathf.Min(otherItem.Stacks, otherItem.Data.MaxStack - otherItem.Stacks);
                otherItem.Stacks += stackAdded;
                stacks -= stackAdded;
                this.OnItemAdded?.Invoke(otherItem, stackAdded);
                if (stacks <= 0) {
                    break;
                }
            }
            // add original item
            if (stacks > 0) {
                stackAdded = Mathf.Min(stacks, item.Data.MaxStack);
                item.Stacks = stackAdded;
                stacks -= stackAdded;
                this._items.Add(item);
                this.OnItemAdded?.Invoke(item, stackAdded);
            }
            // create new items with remaining stacks
            while (stacks > 0) {
                stackAdded = Mathf.Min(stacks, item.Data.MaxStack);
                item = new Item { Instance = item.Instance, Data = item.Data, Stacks = stackAdded };
                stacks -= stackAdded;
                this._items.Add(item);
                this.OnItemAdded?.Invoke(item, stackAdded);
            }
        }

        public void Add(ItemSO item, int amount = 1, bool withNewInstance = true) {
            this.Add(item.GetNewItem(amount, withNewInstance));
        }

        public void Add(Item[] items) {
            foreach (Item item in items) {
                this.Add(item);
            }
        }

        public void Add(IEnumerable<Item> items) {
            foreach (Item item in items) {
                this.Add(item);
            }
        }


        //? REMOVES

        private void Remove(IEnumerable<Item> otherItems, int amount) {
            int stackRemoved;
            foreach (Item otherItem in otherItems) {
                stackRemoved = Mathf.Min(otherItem.Stacks, amount);
                otherItem.Stacks -= stackRemoved;
                if (otherItem.Stacks <= 0) {
                    this._items.Remove(otherItem);
                }
                this.OnItemRemoved?.Invoke(otherItem, stackRemoved);
                amount -= stackRemoved;
                if (amount <= 0) {
                    break;
                }
            }
        }

        public void Remove(Item item, int amount = 1) {
            if (amount >= item.Stacks) {
                this._items.Remove(item);
                this.OnItemAdded?.Invoke(item, item.Stacks);
                amount -= item.Stacks;
                if (amount > 0) {
                    this.Remove(item.Data, amount);
                }
            } else {
                item.Stacks -= amount;
                this.OnItemAdded?.Invoke(item, amount);
            }
        }

        public void Remove(ItemSO item, int amount = 1) {
            this.Remove(this.GetItemsOfType(item), amount);
        }

        public void Remove<T>(int amount = 1) where T : AItem {
            this.Remove(this.GetItemsOfType<T>(), amount);
        }

        public void Remove(Item[] items) {
            foreach (Item item in items) {
                this.Remove(item, item.Stacks);
            }
        }

        public void Remove(IEnumerable<Item> items) {
            foreach (Item item in items) {
                this.Remove(item, item.Stacks);
            }
        }
    }
}