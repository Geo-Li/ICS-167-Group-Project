using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemS : MonoBehaviour
{
    public InventoryManagerS inventoryManager;
    public InventoryItemSO item;

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            inventoryManager.AddItem(item);
        }
    }
}
