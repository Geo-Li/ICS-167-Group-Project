using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject playerPrefab;

    [SerializeField]
    private float minX, maxX, posY, minZ, maxZ;

    private void Start()
    {
        Vector3 randomPosition = new Vector3(Random.Range(minX, maxX), posY, Random.Range(minZ, maxZ));
        PhotonNetwork.Instantiate(playerPrefab.name, randomPosition, Quaternion.identity);
    }
}
