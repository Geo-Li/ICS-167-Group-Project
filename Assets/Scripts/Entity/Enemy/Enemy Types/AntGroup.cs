using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Ant Group with a Hive mindset
 */
public class AntGroup : MonoBehaviour
{
    private List<Ant> m_Ants = new List<Ant>();

    // Prefab objects for the ant troop and ant carrier
    [SerializeField]
    private GameObject m_AntTroopPrefab, m_AntCarrierPrefab;

    private GameObject m_Target;

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
        m_Ants.Add(ant);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateAntList();
        UpdateTarget();
    }

    private void UpdateAntList()
    {
        for (int i = m_Ants.Count - 1; i >= 0; i--)
        {
            Ant temp = m_Ants[i];

            if (temp == null)
                m_Ants.RemoveAt(i);
        }

        if (m_Ants.Count <= 0)
            Destroy(gameObject);
    }

    private void UpdateTarget()
    {
        int i = 0;
        GameObject target = null;

        while (target == null && i < m_Ants.Count)
        {
            GameObject temp = m_Ants[i].Target;
            if (temp != null)
                target = temp;

            i++;
        }

        foreach (Ant ant in m_Ants)
            ant.Target = target;
    }
}
