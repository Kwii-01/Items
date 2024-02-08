using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Items {

    [Serializable]
    public class Loot<T> {
        public float Value;
        public T Data;
    }

    public enum LootTableType {
        Weighted,// Values are summed up and the random is between 0 and the total
        Percent// Value between 0 and 1
    }

    [Serializable]
    public class LootTable<T> {
        delegate Loot<T> LootFunc(List<Loot<T>> loots);

        [Header("Settings")]
        [SerializeField] private LootTableType _type;
        [SerializeField] private bool _uniqueLoot;
        [Space]
        [Header("Tables")]
        [SerializeField] private T[] _guarantedLoot;
        [SerializeField] private Loot<T>[] _loots;


        public IEnumerable<T> Get(int amount) {
            List<T> loots = new List<T>(this._guarantedLoot);
            if (this._type == LootTableType.Weighted) {
                this.LootMany(amount, this._uniqueLoot, ref loots, this.GetSingleWeightedLoot);
            } else {
                this.LootMany(amount, this._uniqueLoot, ref loots, this.GetSinglePercentLoot);
            }
            return loots;
        }

        private void LootMany(int amount, bool uniqueLoot, ref List<T> loots, LootFunc lootFunc) {
            List<Loot<T>> lootList = this._loots.ToList();
            Loot<T> loot;
            while (amount > 0 && (uniqueLoot == false || (uniqueLoot && lootList.Count > 0))) {
                loot = lootFunc(lootList);
                loots.Add(loot.Data);
                if (uniqueLoot) {
                    lootList.Remove(loot);
                }
                amount--;
            }
        }

        private Loot<T> GetSingleWeightedLoot(List<Loot<T>> lootList) {
            float totalWeight = lootList.Sum(loot => loot.Value);
            float weight = Random.Range(0f, totalWeight);
            foreach (Loot<T> loot in lootList) {
                if (weight <= loot.Value) {
                    return loot;
                }
                weight -= loot.Value;
            }
            return null;
        }

        private Loot<T> GetSinglePercentLoot(List<Loot<T>> lootList) {
            float rand = Random.Range(0f, 1f);
            foreach (Loot<T> loot in lootList) {
                if (rand <= loot.Value) {
                    return loot;
                }
            }
            return null;
        }
    }
}