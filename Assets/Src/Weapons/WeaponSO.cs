using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace Weapons {
    public abstract class WeaponSO : ScriptableObject {
        public string Name;
        public Sprite Icon;

        public float useSpeed;
    }
}
