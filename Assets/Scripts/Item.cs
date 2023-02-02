using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Item : MonoBehaviour
{
    //public static event Action OnItemCollected;

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

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Destroy(gameObject);
    //        Debug.Log("Hit");
    //        //OnItemCollected?.Invoke();
    //    }
    //}


    //public void Collect()
    //{
    //    Destroy(gameObject);
    //    //OnItemCollected?.Invoke();
    //}
}
