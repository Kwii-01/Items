
using System.Collections;
using System.Collections.Generic;

using Items;

using UnityEditor;

using UnityEngine;

[InitializeOnLoad]
public static class ItemEditor {
    static ItemEditor() {
        CreateItemsManager();
    }

    private static void CreateResourcesFolder() {
        if (AssetDatabase.IsValidFolder("Assets/Resources/") == false) {
            AssetDatabase.CreateFolder("Assets", "Resources");
        }
    }

    private static void CreateItemsManager() {
        string[] assets = AssetDatabase.FindAssets("t:ItemsManager");
        if (assets.Length == 0) {
            CreateResourcesFolder();
            ItemsManager manager = ScriptableObject.CreateInstance<ItemsManager>();
            AssetDatabase.CreateAsset(manager, "Assets/Resources/ItemsManager.asset");
            AssetDatabase.Refresh();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}
