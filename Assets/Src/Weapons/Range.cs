using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Weapons {
    public class Range : Weapon {
        [SerializeField] private RangeSO _settings;
        public override WeaponSO Settings => this._settings;
        public override bool CanUse => true;
    }
}