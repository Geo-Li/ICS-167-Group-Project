using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemS : MonoBehaviour
{
    [SerializeField] private InventoryManagerS inventoryManager;
    [SerializeField] private InventoryItemSO item;

    // When collide with player, destory the item and store into the inventory
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}
