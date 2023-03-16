using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Will Min

/*
 * Spawn players with photon online functions when entering a game room
 */
public class SpawnPlayers : MonoBehaviourPunCallbacks
{
    // The player prefab used to create players
    [SerializeField]
    private GameObject m_PlayerPrefab;

    // The bounding box where the player will spawn
    [SerializeField]
    private float minX, maxX, posY, minZ, maxZ;

    // the shared health linked to the game objective
    [SerializeField]
    private SharedHealth m_MainHealth;

    private GameObject[] m_PlayerList;

    private int m_SavedPlayerCount;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), posY, Random.Range(minZ, maxZ));
        Entity playerObject = PhotonNetwork.Instantiate(m_PlayerPrefab.name, randomPosition, Quaternion.identity).GetComponent<Entity>();

        m_PlayerList = GameObject.FindGameObjectsWithTag(m_PlayerPrefab.tag);
        m_SavedPlayerCount = 0;
    }

    private void Update()
    {
        if (PhotonNetwork.CountOfPlayers != m_SavedPlayerCount)
        {
            m_PlayerList = GameObject.FindGameObjectsWithTag(m_PlayerPrefab.tag);
            m_SavedPlayerCount = m_PlayerList.Length;
        }

        //DisplayList();

        if (m_PlayerList != null && m_PlayerList.Length > 0 && m_MainHealth != null)
            foreach (GameObject p in m_PlayerList)
            {
                Entity e = p.GetComponent<Entity>();
                if (e != null)
                    m_MainHealth.AddSharingEntity(e);
            }
    }

    private void DisplayList()
    {
        string playerList = "";

        foreach (GameObject p in m_PlayerList)
            playerList += p.name + " ";

        Debug.LogWarning("Count: " + m_PlayerList.Length + " | " + "Real Count: " + PhotonNetwork.CountOfPlayers + " | " + playerList);
    }
}
