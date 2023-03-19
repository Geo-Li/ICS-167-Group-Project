using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Geo Li, Leyna Ho
public class ItemS : MonoBehaviour
{
    private InventoryManagerS inventoryManager;
    [SerializeField] private InventoryItemSO item;

    // When collide with player, destory the item and store into the inventory
    public void OnCollisionEnter(Collision collision)
    {
        inventoryManager = FindObjectOfType<InventoryManagerS>();

        if (collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}
