using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Represents a collectible, its minimum and maximum count per roll, 
 * and the chance of a successful roll for the collectible.
 */
[System.Serializable]
public struct LootDropScript : LootInterface
{
    // The collectible that will drop
    [SerializeField]
    private GameObject m_ItemPrefab;

    // The integer values of the minimum and maximum number of collectibles that can spawn per roll
    [SerializeField]
    private int m_MinCount, m_MaxCount;

    // The probability of this item appearing in the fainl list of drops
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Probability;

    // Generates a random number of m_ItemName and returns it as a list
    public List<GameObject> GenerateLootDrops()
    {
        List<GameObject> result = null;

        if (m_ItemPrefab != null)
        {
            bool willDrop = WillDrop();

            if (willDrop)
            {
                result = new List<GameObject>();
                int count = getRandomCount();

                for (int i = 0; i < count; i++)
                {
                    GameObject temp = GameObject.Instantiate(m_ItemPrefab);
                    result.Add(temp);
                }
            }
        }

        return result;
    }

    // Gets a random count of collectibles from m_MinCount to m_MaxCount
    private int getRandomCount()
    {
        return Random.Range(m_MinCount, m_MaxCount + 1);
    }

    // Rolls to see if this loot will drop at all
    private bool WillDrop()
    {
        return Random.Range(0f, 1f) <= m_Probability;
    }
}
