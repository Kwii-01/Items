using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Entities;

using SavingSystem;

using StatSystem;

using UnityEngine;

using Random = UnityEngine.Random;

namespace Items {
    [Serializable]
    public class EquipmentBuilder : AItem {
        [SerializeField] private Vector2Int _statAmount;
        [SerializeField] private LootTable<RandomizedStatModifierBuilder> _modifiersLootTable;
        public override AItem NewInstance() {
            return new Equipment { Modifiers = this._modifiersLootTable.Get(Random.Range(this._statAmount.x, this._statAmount.y)).Select(m => m.Randomize()).ToArray() };
        }

        public override string Serialize() {
            throw new System.NotImplementedException();
        }

        public override AItem Unserialize(string json) {
            Wrapper<StatModifierBuilder> wrappedData = JsonUtility.FromJson<Wrapper<StatModifierBuilder>>(json);
            return new Equipment { Modifiers = wrappedData.Values };
        }

    }

    public class Equipment : AItem {
        public StatModifierBuilder[] Modifiers;

        public override AItem NewInstance() {
            return new Equipment { Modifiers = this.Modifiers };
        }

        public override string Serialize() {
            return JsonUtility.ToJson(new Wrapper<StatModifierBuilder> { Values = this.Modifiers });
        }

        public override AItem Unserialize(string json) {
            throw new System.NotImplementedException();
        }

        public virtual void Equip(Entity entity) {
            foreach (StatModifierBuilder modifierBuilder in this.Modifiers) {
                entity.Stats.AddModifier(modifierBuilder.StatType, modifierBuilder.Build(this));
            }
        }

        public virtual void UnEquip(Entity entity) {
            foreach (StatModifierBuilder modifierBuilder in this.Modifiers) {
                entity.Stats.RemoveAllModifiersFromSource(modifierBuilder.StatType, this);
            }
        }
    }
}