using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Model for spawning entities
 */

public class EntitySpawner : MonoBehaviour
{
    // The entity prefab that will be spawned
    [SerializeField]
    private GameObject m_EntityPrefab;

    private float m_SpawnerTimer = 0;

    // Minimum and Maximum amount of time in seconds for the next entity to spawn
    [SerializeField]
    private float m_MinTimeRestart, m_MaxTimeRestart;

    // Maximum number of entities the spawner can instantiate
    [SerializeField]
    private int m_MaximumNumberofEntities;

    private List<GameObject> entities = new List<GameObject>();

    private void Start()
    {
        ResetTimer();
    }

    // Spawns the entity prefab
    public GameObject SpawnEntity()
    {
        GameObject spawnedEntity = GameObject.Instantiate(m_EntityPrefab, transform.position, Quaternion.identity);

        entities.Add(spawnedEntity);

        return spawnedEntity;
    }

    private void FixedUpdate()
    {
        UpdateTimer();
        UpdateList();
        SpawnOnTimer();
    }

    private void UpdateTimer()
    {
        if (m_SpawnerTimer > 0)
            m_SpawnerTimer -= Time.deltaTime;
    }

    private void UpdateList()
    {
        for (int i = entities.Count - 1; i >= 0; i--)
        {
            GameObject temp = entities[i];
            if (temp == null)
                entities.RemoveAt(i);
        }
    }

    private void SpawnOnTimer()
    {
        if (entities.Count >= m_MaximumNumberofEntities || m_SpawnerTimer > 0)
            return;

        SpawnEntity();

        ResetTimer();
    }

    private void ResetTimer()
    {
        m_SpawnerTimer = Random.Range(m_MinTimeRestart, m_MaxTimeRestart);
    }

    // Test spawning on button
    public void TestSpawnEntity()
    {
        SpawnEntity();
    }
}
