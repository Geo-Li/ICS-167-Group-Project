using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// William Min

/*
 * Customizes the interface of LottTable Scriptable Object
 * 
 * Includes the following changes:
 * + A button to generate a string of the list of random drops
 */
[CustomEditor(typeof(LootTable))]
[CanEditMultipleObjects]
public class LootTableEditor : Editor
{
    // Displays the button that generates a string of the list of random drops
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
