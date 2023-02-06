using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    //public static event Action OnItemCollected;

    //if items collide with the player they will destroy themselves and create an event message
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Collector player = collision.GetComponent<Collector>();
    //    if (player != null)
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("Hit");
    //        //OnItemCollected?.Invoke();
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Collector player = collision.GetComponent<Collector>();
        if (player != null)
        {
            Destroy(gameObject);
            Debug.Log("Hit");
            //OnItemCollected?.Invoke();
        }
    }

}
