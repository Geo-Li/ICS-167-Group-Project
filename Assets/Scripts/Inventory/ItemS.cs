using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Geo Li, Leyna Ho
public class ItemS : MonoBehaviour
{
    private InventoryManagerS inventoryManager;
    [SerializeField] private InventoryItemSO item;

    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManagerS>();
    }

    // When collide with player, destory the item and store into the inventory
    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player")
        {
            inventoryManager.AddItem(item);
            Destroy(gameObject);
        }
    }
}
