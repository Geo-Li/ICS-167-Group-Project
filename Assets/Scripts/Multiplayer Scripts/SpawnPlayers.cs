using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Will Min

/*
 * Spawn players with photon online functions when entering a game room
 */
public class SpawnPlayers : MonoBehaviour
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

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), posY, Random.Range(minZ, maxZ));
        Entity playerObject = PhotonNetwork.Instantiate(m_PlayerPrefab.name, randomPosition, Quaternion.identity).GetComponent<Entity>();

        if (m_MainHealth != null && playerObject != null)
            m_MainHealth.AddSharingEntity(playerObject);
    }
}
