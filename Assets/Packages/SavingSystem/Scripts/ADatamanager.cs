using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SavingSystem {
    public abstract class ADatamanager : MonoBehaviour {
        [SerializeField] protected ASaver saver;
    }
}