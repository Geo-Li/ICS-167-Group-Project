using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Geo Li, Leyna Ho

public class InventoryManagerS : MonoBehaviour
{
    [SerializeField] private GameObject[] inventorySlots;
    [SerializeField] private SharedHealth player;
    [SerializeField] private int itemHeal = 10;

    // Add the collected item to a specific slot
    public void AddItem(InventoryItemSO item)
    {
        //if (!PhotonNetwork.IsMasterClient)
            //return;

        // Check which slot this item belongs to
        for (int i = 0; i < inventorySlots.Length; i++) 
        {
            //GameObject slot = inventorySlots[i];
            InventoryItemS itemInSlot = inventorySlots[i].GetComponentInChildren<InventoryItemS>();

            if (item.GetInventoryItemSOImage() == itemInSlot.GetItemSprite())
            {
                itemInSlot.IncreaseCount();
                return;
                /*
                // if there is no item in slot, then initialize it
                if (itemInSlot.GetCount() < 1)
                {
                    SpawnNewItem(slot);
                    return;
                }
                else
                {
                    itemInSlot.IncreaseCount();
                    itemInSlot.RefreshCount();
                    return;
                }
                */
            }
        }
    }

    // Uses the collected item in a specific slot
    public void UseItem(int slotIndex)
    {
        //if (!PhotonNetwork.IsMasterClient)
            //return;

        if (slotIndex < 0 || slotIndex >= inventorySlots.Length)
            return;

        InventoryItemS itemInSlot = inventorySlots[slotIndex].GetComponentInChildren<InventoryItemS>();

        if (itemInSlot == null)
            return;

        bool LacksHealth = !player.Health.IsAtFullHealth();

        // add new condition to prevent player using item if they have full HP
        if (itemInSlot.GetCount() > 1 && LacksHealth)
        {
            itemInSlot.DecreaseCount();
            player.Health.CurrentHealth += itemHeal;
        }
        else if (itemInSlot.GetCount() == 1 && LacksHealth)
        {
            player.Health.CurrentHealth += itemHeal;
            itemInSlot.ResetAlphaWhenZero();
        }
    }

    /*
    // show the item to the slot
    void SpawnNewItem(InventoryItemS inventoryItem)
    {
        if (inventoryItem != null)
            inventoryItem.IncreaseCount();
    }
    */
}
