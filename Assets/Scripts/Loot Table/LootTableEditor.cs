using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// William Min
[CustomEditor(typeof(LootTable))]
[CanEditMultipleObjects]
public class LootTableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        var tool = target as LootTable;
        serializedObject.Update();

        if (GUILayout.Button("Generate Loot"))
        {
            tool.DisplayLootDrops();
        }
    }
}
