using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScript : MonoBehaviour
{
    public InventoryManagerS inventoryManager;
    public InventoryItemSO[] items;

    public void PickupItem(int id){
        inventoryManager.AddItem(items[id]);
    }

}
