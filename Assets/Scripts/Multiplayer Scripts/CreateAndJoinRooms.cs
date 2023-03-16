using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

// Will Min

/*
 * Script for creating and joining game rooms
 */
public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    // Input fields for creating or joining online rooms
    [SerializeField]
    private TMP_InputField createInput, joinInput;

    // The game scene used for the rooms
    [SerializeField]
    private string m_GameSceneName;

    // Create a multiplayer room
    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(createInput.text);
    }

    // Create a singleplayer room
    public void CreateSinglePlayerRoom()
    {
        PhotonNetwork.CreateRoom("single");
    }

    // Join a multiplayer room
    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
    }

    // Generate the game scene when creating or joining a room
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(m_GameSceneName);
    }
}
