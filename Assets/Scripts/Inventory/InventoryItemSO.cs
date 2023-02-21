using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "Scriptable Objects/Inventory Item")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField] private Sprite image;
    [SerializeField] private string name;
    [SerializeField] private int initialCount = 0;

    // Getter functions for above variables
    public Sprite GetInventoryItemSOImage() {
        return image;
    }

    public string GetInventoryItemSOName() {
        return name;
    }

    public int GetInventoryItemSOInitialCount() {
        return initialCount;
    }
}
