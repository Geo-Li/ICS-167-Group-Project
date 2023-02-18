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
        // prove this by not using for loop
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
        // add new condition to prevent player using item if they have full HP
        if (itemInSlot.count > 1 && playerCurrHealth < 100) {
            itemInSlot.count--;
            player.playerHealth = playerCurrHealth + 10;
            healthBar.SetHealth(playerCurrHealth + 10);
            itemInSlot.RefreshCount();
        } else if (itemInSlot.count == 1 && playerCurrHealth < 100) { // if the count is equal to one
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
