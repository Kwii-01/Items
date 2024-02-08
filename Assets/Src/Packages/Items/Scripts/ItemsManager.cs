using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using AYellowpaper.SerializedCollections;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Items {
    public class ItemsManager : ScriptableObject {
        private static ItemsManager _instance = default;
        public static ItemsManager Instance => _instance;


        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void InitRuntimeInstance() {
            _instance = Resources.Load<ItemsManager>("ItemsManager.asset");
            _instance._itemDico = _instance._items.ToDictionary(i => i.Guid);
        }

        [SerializeField] private List<ItemSO> _items;
        private Dictionary<string, ItemSO> _itemDico;

        public ItemUI ItemUIPrefab;


        public bool Has(ItemSO itemSO) {
            return this._items.Contains(itemSO);
        }

        public ItemSO Get(string guid) {
            return this._itemDico[guid];
        }

        public bool TryGetValue(string guid, out ItemSO itemSO) {
            return this._itemDico.TryGetValue(guid, out itemSO);
        }

        public ItemUI CreateItemUI(Item item, Transform parent) {
            ItemUI ui = Instantiate(ItemsManager.Instance.ItemUIPrefab, parent);
            ui.Set(item);
            return ui;
        }
    }
}