using System;
using System.Collections.Generic;

using UnityEngine;

using Random = UnityEngine.Random;

namespace StatSystem {

    public enum StatModType {
        Flat = 100,
        PercentAdd = 200,
        PercentMult = 300,
    }

    [Serializable]
    public class StatModifier {
        public float Value;
        public StatModType Type = StatModType.Flat;
        public int Order;
        public object Source;

        public StatModifier() {
        }

        public StatModifier(float value, StatModType type, int order, object source) : this() {
            Value = value;
            Type = type;
            Order = order;
            Source = source;
        }

        public StatModifier(float value, StatModType type) : this(value, type, (int)type, null) { }
        public StatModifier(float value, StatModType type, int order) : this(value, type, order, null) { }
        public StatModifier(float value, StatModType type, object source) : this(value, type, (int)type, source) { }

    }

    public abstract class AStatModifierBuilder<T> {
        public StatType StatType;
        public T Value;
        public StatModType Type = StatModType.Flat;

        public abstract StatModifier Build(object source);
    }

    [Serializable]
    public class RandomizedStatModifierBuilder : AStatModifierBuilder<Vector2> {
        public override StatModifier Build(object source) {
            return new StatModifier(Random.Range(this.Value.x, this.Value.y), this.Type, source);
        }

        public StatModifierBuilder Randomize() {
            return new StatModifierBuilder { StatType = this.StatType, Value = Random.Range(this.Value.x, this.Value.y), Type = this.Type };
        }
    }

    [Serializable]
    public class StatModifierBuilder : AStatModifierBuilder<float> {
        public override StatModifier Build(object source) {
            return new StatModifier(this.Value, this.Type, source);
        }
    }



}