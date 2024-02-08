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

    public interface IEquipable {
        public void UnEquip(Entity entity);
        public void Equip(Entity entity);
    }

    public abstract class Equipment : AItem, IEquipable {

        public StatModifierBuilder[] Modifiers;

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

    public class EquipmentBuilder<T> : AItem where T : Equipment, new() {
        [SerializeField] protected Vector2Int _additionalStats;
        [SerializeField] protected LootTable<RandomizedStatModifierBuilder> _modifiersLootTable;

        public override AItem NewInstance() {
            int modifierAmount = Random.Range(this._additionalStats.x, this._additionalStats.y);
            return new T { Modifiers = this._modifiersLootTable.Get(modifierAmount).Where(m => m != null).Select(m => m.Randomize()).ToArray() };
        }

        public override string Serialize() {
            throw new System.NotImplementedException();
        }

        public override AItem Unserialize(string json) {
            Wrapper<StatModifierBuilder> wrappedData = JsonUtility.FromJson<Wrapper<StatModifierBuilder>>(json);
            return new T { Modifiers = wrappedData.Values };
        }

    }
}