using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Entities;
using System;

namespace Items {
    public interface IEquipment {
        public EquipmentType Type {
            get;
        }
        public void Equip(Entity entity);
        public void Unequip(Entity entity);
    }

    public enum EquipmentType {
        None,
        WeaponRL,//in right hand and left as second hand
        WeaponLR,//in left hand and right as second hand
        WeaponR,//in right hand
        WeaponL,//in left hand
        Head,
        Torso,
        Pant,
        Shoes,
    }
}