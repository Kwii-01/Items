using System;

namespace Items {
    [Serializable]
    public class WeaponBuilder : EquipmentBuilder<Weapon> {

    }

    public class Weapon : Equipment {
        public override AItem NewInstance() {
            return new Weapon { Modifiers = this.Modifiers };
        }
    }
}