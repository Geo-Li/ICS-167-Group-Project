using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> inventory = new List<InventoryItem>();
    private Dictionary<ItemData, InventoryItem> itemDictionary = new Dictionary<ItemData, InventoryItem>();

    public void OnEnable()
    {
        Item.OnItemCollected += Add;
    }

    public void OnDisable()
    {
        Item.OnItemCollected -= Add;
    }

    public void Add(ItemData itemData)
    {
        //if item is in dictionary add it too the stack
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.AddToStack();
            Debug.Log($"{item.itemData.displayName} total stack is now {item.stackSize}");
        }
        //if item is not in dictionary add it to the dictionary
        else
        {
            InventoryItem newItem = new InventoryItem(itemData);
            inventory.Add(newItem);
            itemDictionary.Add(itemData, newItem);
            Debug.Log($"Added {itemData.displayName} to the inventory for the first time");
        }
    }

    public void Remove(ItemData itemData)
    {
        //removes item from stack
        if(itemDictionary.TryGetValue(itemData, out InventoryItem item))
        {
            item.RemoveFromStack();
            //if the stack is 0 remove the item from the inventory and dictionary
            if(item.stackSize == 0)
            {
                inventory.Remove(item);
                itemDictionary.Remove(itemData);
            }
        }
    }
}
