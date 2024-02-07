using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Weapons {
    public class Melee : Weapon {
        [SerializeField] private MeleeSO _settings;
        public override WeaponSO Settings => this._settings;
        public override bool CanUse => true;
    }
}