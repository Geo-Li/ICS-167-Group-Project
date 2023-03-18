using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


// Geo Li, Leyna Ho
[CreateAssetMenu(menuName = "Scriptable Objects/Inventory Item")]
public class InventoryItemSO : ScriptableObject
{
    [SerializeField] private Sprite image;
    //[SerializeField] private string itemName;
    //[SerializeField] private int initialCount = 0;

    // Getter functions for above variables
    public Sprite GetInventoryItemSOImage() {
        return image;
    }

    /*
    // Returns the name of the item
    public string GetInventoryItemSOName() {
        return itemName;
    }

    // Returns the initial count
    public int GetInventoryItemSOInitialCount() {
        return initialCount;
    }
    */
}
