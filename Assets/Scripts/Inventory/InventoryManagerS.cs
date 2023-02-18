using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerS : MonoBehaviour
{
    public InventorySlotS[] inventorySlots;
    public HealthBarS healthBar;
    public PlayerHealthS player;

    // add the collected item to the specific slot
    public void AddItem(InventoryItemSO item)
    {
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlotS slot = inventorySlots[i];
            InventoryItemS itemInSlot = slot.GetComponentInChildren<InventoryItemS>();
            // Debug.Log(itemInSlot.count);
            if (item.name == itemInSlot.name) {
                if (itemInSlot.count < 1) {
                    SpawnNewItem(item, slot);
                    return;
                } else {
                    // Debug.Log(itemInSlot.count);
                    itemInSlot.count++;
                    itemInSlot.RefreshCount();
                    return;
                }
            }
        }
    }

    // add the collected item to the specific slot
    public void UseItem(InventorySlotS slot)
    {
        int playerCurrHealth = player.playerHealth;
        InventoryItemS itemInSlot = slot.GetComponentInChildren<InventoryItemS>();
        // Debug.Log(itemInSlot.count);
        if (itemInSlot.count > 1) {
            itemInSlot.count--; // this line is lil buggy
            player.playerHealth = playerCurrHealth + 10;
            healthBar.SetHealth(playerCurrHealth + 10);
            itemInSlot.RefreshCount();
        } else if (itemInSlot.count == 1) { // if the count is equal to one
            Debug.Log("here");
            player.playerHealth = playerCurrHealth + 10;
            healthBar.SetHealth(playerCurrHealth + 10);
            itemInSlot.ResetAlphaWhenZero();
        }
    }

    // show the item to the slot
    void SpawnNewItem(InventoryItemSO item, InventorySlotS slot)
    {
        // GameObject newItemGO = Instantiate(inventoryItemPrefab, slot.transform);
        InventoryItemS inventoryItem = slot.GetComponentInChildren<InventoryItemS>();
        inventoryItem.InitializeItem(item);
    }
}
