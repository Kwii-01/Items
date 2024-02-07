using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Entities;

using SavingSystem;

using StatSystem;

using UnityEngine;

namespace Items {
    [Serializable]
    public class EquipmentBuilder : AItem {
        [SerializeField] private RandomizedStatModifierBuilder[] _modifiers;
        public override AItem NewInstance() {
            return new Equipment { Modifiers = this._modifiers.Select(m => m.Randomize()).ToArray() };
        }

        public override string Serialize() {
            return this.NewInstance().Serialize();
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