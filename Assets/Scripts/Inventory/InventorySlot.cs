using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

// Will Min

/*
 * Representation of inventory slot gameobject
 */
public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private InventoryItemSO m_ScriptableObject;

    [SerializeField]
    private GameObject m_ItemPrefab;

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
            return;

        InventoryItemS obj = PhotonNetwork.Instantiate(m_ItemPrefab.name, Vector3.zero, Quaternion.identity).GetComponent<InventoryItemS>();
        obj.Initialize(m_ScriptableObject.GetInventoryItemSOImage());

        obj.transform.parent = this.transform;

        obj.transform.localScale = Vector3.one * 1.2f;
    }
}
