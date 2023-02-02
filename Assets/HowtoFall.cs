using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowtoFall : MonoBehaviour
{


    // private GameObject[] floors = new GameObject[5];
    // [SerializeField] float delay = 1f;
    // [SerializeField] float respawnDelay = 10f;
    // Vector2 initScale;

    // void activateFloor(GameObject floor) {
    //     floor.SetActive(true);
    // }

    // void deactivateFloor(GameObject floor) {
    //     floor.SetActive(false);
    // }

    void OnCollisionEnter(Collision collision) {
        // initScale = transform.localScale;
        // float playerPosY = collision.gameObject.transform.position.y - collision.gameObject.transform.localScale.y / 2;
        // float floorPosY = transform.position.y + initScale.y / 2;
        if (collision.gameObject.tag == "Player") {
            // set the timer to deactivate the floor, and respawn it in 5 seconds
            Debug.Log(collision.transform);
            // Vector2 direction = c.GetContact(0).normal;
            // if( direction.x == 1 ) {Debug.Log("Player hits: “right”");}
            // if( direction.x == -1 ) {Debug.Log("Player hits: “left”");}
            // if( direction.y == 1 ) {Debug.Log("Player hits: up");}
            // if( direction.y == -1 ) {Debug.Log("Player hits: down");}
            // Debug.Log("Player hits: ", );
            // StartCoroutine(delayDeactivate(delay));
            // StartCoroutine(respawn(respawnDelay));
        }
    }

    // IEnumerator delayDeactivate(float _delay) {
    //     yield return new WaitForSeconds(_delay);
    //     transform.localScale = Vector2.zero;
    //     // this.gameObject.SetActive(false);
    // }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
