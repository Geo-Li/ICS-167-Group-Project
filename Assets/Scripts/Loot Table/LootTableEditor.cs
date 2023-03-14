using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
//Game will not build if it tries to run code for the unity engine which stops existing
using UnityEditor;
#endif

// William Min

/*
 * Customizes the interface of LottTable Scriptable Object
 * 
 * Includes the following changes:
 * + A button to generate a string of the list of random drops
 */
#if UNITY_EDITOR
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
#endif
