using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;


// Geo Li, Leyna Ho
public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;

    // Start is called before the first frame update
    void Start()
    {
        if (MainMenu.isMultiplayer) {
            player1Prefab.SetActive(true);
            player2Prefab.SetActive(true);
        } else {
            player1Prefab.SetActive(true);
            player2Prefab.SetActive(false);
        }
    }
}
