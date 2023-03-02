using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowPlayerSpeed : MonoBehaviour
{
    public Text playerSpeedText;
    public PlayerMovement player;


    void Update() {

        playerSpeedText.text = player.GetCurrentSpeed().ToString();
        //playerSpeedText.text = player.GetVelocity().ToString();

    }

}
