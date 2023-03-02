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

    // Spawns the entity prefab
    public GameObject SpawnEntity()
    {
        GameObject spawnedEntity = GameObject.Instantiate(m_EntityPrefab, transform.position, Quaternion.identity);

        return spawnedEntity;
    }

    // Test spawning on button
    public void TestSpawnEntity()
    {
        SpawnEntity();
    }
}
