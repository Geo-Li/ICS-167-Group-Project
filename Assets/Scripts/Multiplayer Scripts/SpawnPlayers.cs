using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PlayerPrefab;

    [SerializeField]
    private float minX, maxX, posY, minZ, maxZ;

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
