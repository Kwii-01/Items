using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using UnityEditor;
using UnityEditor.SceneManagement;

namespace Items {

    [CustomEditor(typeof(Inventory))]
    public class InventoryEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            GUILayout.Space(5f);
            if (GUILayout.Button("Generate save key")) {
                EditorUtility.SetDirty(this.target);
                EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
                (this.target as Inventory).Reset();
            }
        }
    }

}