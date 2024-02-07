using System.Collections;
using System.Collections.Generic;

using Entities;

using Items;

using UnityEngine;

namespace Weapons {
    public abstract class Weapon : MonoBehaviour, IEquipment {
        [SerializeField] private EquipmentType _type;

        public EquipmentType Type => this._type;
        public abstract WeaponSO Settings {
            get;
        }
        public abstract bool CanUse {
            get;
        }

        protected Entity user;
        protected float useTimer;

        public virtual void Equip(Entity entity) {
            this.user = entity;
        }

        public virtual void Unequip(Entity entity) {
            this.user = null;
        }

        public void UseStarted() {
        }

        public void UseEnded() {

        }
    }
}