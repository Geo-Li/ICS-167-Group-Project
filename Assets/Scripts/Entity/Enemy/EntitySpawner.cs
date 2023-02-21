using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Model for spawning entities
 */

public class EntitySpawner : MonoBehaviour
{
    // Entity Types in form of enum states
    private enum EntityType
    {
        ENTITY,
        PROP,
        ENEMY
    }

    // The entity prefab that will be spawned
    [SerializeField]
    private Entity m_EntityPrefab;

    // Entity type
    [SerializeField]
    private EntityType m_EntityType;

    // Spawns the entity prefab
    public Entity SpawnEntity(GameObject target)
    {
        Entity spawnedEntity = GameObject.Instantiate(m_EntityPrefab, transform.position, Quaternion.identity);

        if (m_EntityType == EntityType.ENEMY)
        {
            EnemyMovement movement = spawnedEntity.gameObject.GetComponent<EnemyMovement>();

            if (movement != null)
                movement.Target = target;
        }

        return spawnedEntity;
    }

    // Test spawning on button
    public void TestSpawnEntity(GameObject target)
    {
        SpawnEntity(target);
    }
}
