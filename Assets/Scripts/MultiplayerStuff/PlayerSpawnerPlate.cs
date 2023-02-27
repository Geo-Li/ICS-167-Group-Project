using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnerPlate : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate two players using a split-keyboard setup.
        var player1 = PlayerInput.Instantiate(prefab: playerPrefab, playerIndex: 0, controlScheme: "WASD keyboard", pairWithDevice: Keyboard.current, splitScreenIndex: 0);
        var player2 = PlayerInput.Instantiate(prefab: playerPrefab, playerIndex: 1, controlScheme: "Arrows keyboard", pairWithDevice: Keyboard.current, splitScreenIndex: 1);
    }
}
