using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    public static event HandleItemCollected OnItemCollected;
    public delegate void HandleItemCollected(ItemData itemData);
    public ItemData dataOfItem;

    //if items collide with the player they will destroy themselves and create an event message
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            OnItemCollected?.Invoke(dataOfItem);
        }
    }

}
