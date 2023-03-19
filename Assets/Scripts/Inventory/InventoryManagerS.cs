using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Geo Li, Leyna Ho

public class InventoryManagerS : MonoBehaviourPun
{
    [SerializeField] private GameObject[] inventorySlots;
    [SerializeField] private SharedHealth player;
    [SerializeField] private int itemHeal = 10;

    // Add the collected item to a specific slot
    public void AddItem(InventoryItemSO item)
    {
        // Check which slot this item belongs to
        for (int i = 0; i < inventorySlots.Length; i++) 
        {
            InventoryItemS itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItemS>();

            if (item.GetInventoryItemSOImage() == itemInSlot.GetItemSprite())
            {
                itemInSlot.IncreaseCount();
                return;
            }
        }
    }

    // Uses the collected item in a specific slot
    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= inventorySlots.Length)
            return;

        InventoryItemS itemInSlot = inventorySlots[slotIndex].GetComponentInChildren<InventoryItemS>();

        if (itemInSlot == null)
            return;

        bool LacksHealth = !player.Health.IsAtFullHealth();

        Debug.Log(itemInSlot.GetCount());

        // add new condition to prevent player using item if they have full HP
        if (itemInSlot.GetCount() > 0 && !player.Health.IsAtFullHealth())//LacksHealth)
        {
            itemInSlot.DecreaseCount();
            player.Health.CurrentHealth += itemHeal;
        }
        /*
        else if (itemInSlot.GetCount() == 1 && LacksHealth)
        {
            player.Health.CurrentHealth += itemHeal;
            itemInSlot.ResetAlphaWhenZero();
        }
        */
    }

}
