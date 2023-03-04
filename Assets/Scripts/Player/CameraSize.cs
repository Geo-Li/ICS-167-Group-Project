using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Geo Li
public class CameraSize : MonoBehaviour
{

    [SerializeField] private Camera cam;
    [SerializeField] private PlayerSpawner playerSpawner;
    [SerializeField] private int playerNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        if (playerNumber == 1) {
            if (MainMenu.isMultiplayer) {
                cam.rect = new Rect(0f, 0f, 0.5f, 1f);
            } else {
                cam.rect = new Rect(0f, 0f, 1f, 1f);
            }
        } else if (playerNumber == 2) {
            if (MainMenu.isMultiplayer) {
                cam.rect = new Rect(0.5f, 0f, 0.5f, 1f);
            } else {
                cam.rect = new Rect(0f, 0f, 1f, 1f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
