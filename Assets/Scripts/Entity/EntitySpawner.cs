using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


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
    private float m_MinTimeRestart = 0, m_MaxTimeRestart = Mathf.Infinity;

    // Maximum number of entities the spawner can instantiate
    [SerializeField]
    private int m_MaximumNumberofEntities;

    private List<GameObject> entities = new List<GameObject>();

    // Dimensions of 2D bounding box of spawn area
    [SerializeField]
    private float m_SpawnAreaLength, m_SpawnAreaWidth;

    private void Start()
    {
        ResetTimer();
    }

    // Spawns the entity prefab
    public GameObject SpawnEntity(Vector3 position)
    {
        GameObject spawnedEntity = GameObject.Instantiate(m_EntityPrefab, position, Quaternion.identity);

        entities.Add(spawnedEntity);

        return spawnedEntity;
    }

    private GameObject SpawnEntityOnRandomPosition()
    {
        Vector3 pos = Vector3.zero;
        bool hasFoundPoint = false;
        int i = 0;

        while (!hasFoundPoint && i < 30)
        {
            pos = transform.position;

            float halfLength = m_SpawnAreaLength / 2;
            float x = Random.Range(-halfLength, halfLength);

            float halfWidth = m_SpawnAreaWidth / 2;
            float y = Random.Range(-halfWidth, halfWidth);

            pos += new Vector3(x, 0, y);

            Debug.Log(pos);

            hasFoundPoint = PointOnMavMesh(pos);
            i++;
        }

        return SpawnEntity(pos);
    }

    private bool PointOnMavMesh(Vector3 vector)
    {
        NavMeshHit hit;
        return NavMesh.SamplePosition(vector, out hit, 1.0f, NavMesh.AllAreas);
    }

    // Test spawning on button
    public void TestSpawnEntity()
    {
        SpawnEntityOnRandomPosition();
    }

    private void Update()
    {
        DrawSpawnArea();
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

        SpawnEntityOnRandomPosition();

        ResetTimer();
    }

    private void ResetTimer()
    {
        m_SpawnerTimer = Random.Range(m_MinTimeRestart, m_MaxTimeRestart);
    }

    private void DrawSpawnArea()
    {
        Color lengthColor = Color.red;
        Color widthColor = Color.blue;

        float halfLength = m_SpawnAreaLength / 2;
        float x1 = -halfLength;
        float x2 = halfLength;

        float halfWidth = m_SpawnAreaWidth / 2;
        float y1 = -halfWidth;
        float y2 = halfWidth;

        Vector3 p1 = new Vector3(x1, 0, y1) + transform.position;
        Vector3 p2 = new Vector3(x1, 0, y2) + transform.position;
        Vector3 p3 = new Vector3(x2, 0, y1) + transform.position;
        Vector3 p4 = new Vector3(x2, 0, y2) + transform.position;

        Debug.DrawLine(p1, p2, widthColor);
        Debug.DrawLine(p1, p3, lengthColor);
        Debug.DrawLine(p4, p2, lengthColor);
        Debug.DrawLine(p4, p3, widthColor);
    }
}
