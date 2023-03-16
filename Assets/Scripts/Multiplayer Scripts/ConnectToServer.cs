using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

// Will Min

/*
 * Script to connect client to the main menu lobby
 */
public class ConnectToServer : MonoBehaviourPunCallbacks
{
    // Name of the Main Menu Scene
    [SerializeField]
    private string m_LobbySceneName;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartLoading());
    }

    private IEnumerator StartLoading()
    {
        Debug.Log(PhotonNetwork.IsConnected);

        if (PhotonNetwork.IsConnected)
            PhotonNetwork.Disconnect();

        yield return new WaitForSeconds(.1f);

        Debug.Log(PhotonNetwork.IsConnected);

        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene(m_LobbySceneName);
    }
}
