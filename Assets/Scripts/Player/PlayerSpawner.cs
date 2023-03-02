using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] private GameObject player1Prefab;
    [SerializeField] private GameObject player2Prefab;
    [SerializeField] private bool isMultiplayer = false;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate two players using a split-keyboard setup.
        // var player1 = PlayerInput.Instantiate(playerPrefab, playerIndex: 0, controlScheme: "WASD Keyboard", pairWithDevice: Keyboard.current);
        // var player2 = PlayerInput.Instantiate(playerPrefab, playerIndex: 1, controlScheme: "Arrows Keyboard", pairWithDevice: Keyboard.current);
        if (isMultiplayer) {
            player1Prefab.SetActive(true);
            player2Prefab.SetActive(true);
        } else {
            player1Prefab.SetActive(true);
            player2Prefab.SetActive(false);
        }
    }

    public bool IsMultiPlayer() {
        return isMultiplayer;
    }
}
