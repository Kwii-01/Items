using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace SavingSystem {
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(PPSaver))]
    public class PlayerPrefSaverEditor : Editor {
        private string _key;

        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            GUILayout.Space(20f);
            if (GUILayout.Button("RESET DATAS")) {
                (this.target as PPSaver).DeleteAll();
            }
            GUILayout.Space(7f);
            GUILayout.Label("Key reseter");
            GUILayout.Space(2f);
            this._key = GUILayout.TextField(this._key);
            if (GUILayout.Button("RESET KEY")) {
                (this.target as PPSaver).DeleteKey(this._key);
            }
        }
    }
}
