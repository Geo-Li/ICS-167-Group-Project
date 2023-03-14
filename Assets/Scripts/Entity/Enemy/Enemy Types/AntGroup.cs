using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Ant Group with a Hive mindset
 */
public class AntGroup : MonoBehaviour
{
    // Prefab objects for the ant troop and ant carrier
    [SerializeField]
    private GameObject m_AntTroopPrefab, m_AntCarrierPrefab;

    // The range of the possible count of ant troops and carriers that can spawn per group
    [SerializeField]
    private int m_AntTroopCountMin = 4, m_AntTroopCountMax = 6, m_AntCarrierCountMin = 2, m_AntCarrierCountMax = 3;

    // The radius that makes the circle area that an ant can spawn in the group
    [SerializeField]
    private float m_GroupRadius = 5;

    // Start is called before the first frame update
    private void Start()
    {
        int troopCount = (int)Random.Range(m_AntTroopCountMin, m_AntTroopCountMax + 1);
        int carrierCount = (int)Random.Range(m_AntCarrierCountMin, m_AntCarrierCountMax + 1);

        Vector3 randomPoint = transform.position + Random.insideUnitSphere * m_GroupRadius;
        randomPoint = EnemyMovement.GetClosestPointOnNavMesh(randomPoint, m_GroupRadius);

        for (int i = 0; i < troopCount; i++)
            AddAnt(m_AntTroopPrefab);

        for (int i = 0; i < carrierCount; i++)
            AddAnt(m_AntCarrierPrefab);
    }

    private void AddAnt(GameObject antPrefab)
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * m_GroupRadius;
        randomPoint = EnemyMovement.GetClosestPointOnNavMesh(randomPoint, m_GroupRadius);

        Ant ant = GameObject.Instantiate(antPrefab, randomPoint, Quaternion.identity).GetComponent<Ant>();
        ant.transform.parent = this.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        DestroyOnEmpty();
        UpdateTarget();
    }

    private void DestroyOnEmpty()
    {
        if (GetComponentsInChildren<Ant>().Length <= 0)
            Destroy(gameObject);
    }

    private void UpdateTarget()
    {
        Ant[] ants = GetComponentsInChildren<Ant>();
        int i = 0;
        GameObject target = null;

        while (target == null && i < ants.Length)
        {
            GameObject temp = ants[i].Target;
            if (temp != null)
                target = temp;

            i++;
        }

        foreach (Ant ant in ants)
            ant.Target = target;
    }
}
