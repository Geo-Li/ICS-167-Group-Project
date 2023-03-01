using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManagerS : MonoBehaviour
{
    [SerializeField] private InventorySlotS[] inventorySlots;
    [SerializeField] private HealthBarS healthBar;
    [SerializeField] private PlayerHealthS player;
    [SerializeField] private int itemHeal = 10;

    // add the collected item to the specific slot
    public void AddItem(InventoryItemSO item)
    {
        // Check which slot this item belongs to
        for (int i = 0; i < inventorySlots.Length; i++) {
            InventorySlotS slot = inventorySlots[i];
            InventoryItemS itemInSlot = slot.GetComponentInChildren<InventoryItemS>();
            if (item.GetInventoryItemSOName() == itemInSlot.GetInventoryItemName()) {
                // if there is no item in slot, then initialize it
                if (itemInSlot.GetCount() < 1) {
                    SpawnNewItem(item, slot);
                    return;
                } else {
                    itemInSlot.IncreaseCount();
                    itemInSlot.RefreshCount();
                    return;
                }
            }
        }
    }

    // add the collected item to the specific slot
    public void UseItem(InventorySlotS slot)
    {
        int playerCurrHealth = player.GetPlayerHealth();
        InventoryItemS itemInSlot = slot.GetComponentInChildren<InventoryItemS>();
        // add new condition to prevent player using item if they have full HP
        if (itemInSlot.GetCount() > 1 && playerCurrHealth < player.GetPlayerMaxHealth()) {
            itemInSlot.DecreaseCount();
            player.IncreasePlayerHealth(itemHeal);
            healthBar.SetHealth(playerCurrHealth + itemHeal);
            itemInSlot.RefreshCount();
        } else if (itemInSlot.GetCount() == 1 && playerCurrHealth < player.GetPlayerMaxHealth()) { // if the count is equal to one
            player.IncreasePlayerHealth(itemHeal);
            healthBar.SetHealth(playerCurrHealth + itemHeal);
            itemInSlot.ResetAlphaWhenZero();
        }
    }

    // show the item to the slot
    void SpawnNewItem(InventoryItemSO item, InventorySlotS slot)
    {
        InventoryItemS inventoryItem = slot.GetComponentInChildren<InventoryItemS>();
        inventoryItem.InitializeItem(item);
    }
}
