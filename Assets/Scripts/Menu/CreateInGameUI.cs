using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Will Min

/*
 * 
 */
public class CreateInGameUI : MonoBehaviour
{
    [SerializeField]
    private GameObject UIPackage;

    private GameObject instanceUI;

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && instanceUI == null)
            instanceUI = PhotonNetwork.Instantiate(UIPackage.name, Vector3.zero, Quaternion.identity);
    }
}
