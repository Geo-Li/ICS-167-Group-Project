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
    private string m_ItemName;

    // The integer values of the minimum and maximum number of collectibles that can spawn per roll
    [SerializeField]
    private int m_MinCount, m_MaxCount;

    // The probability of this item appearing in the fainl list of drops
    [SerializeField]
    [Range(0f, 1f)]
    private float m_Probability;

    // Generates a random number of m_ItemName and returns it as a list
    // (Temporarily as string, but will be actual collectible objects later on)
    public List<string> GenerateLootDrops()
    {
        List<string> result = null;

        bool willDrop = WillDrop();

        if (willDrop)
        {
            result = new List<string>();
            int count = getRandomCount();

            for (int i = 0; i < count; i++)
            {
                string temp = (string)m_ItemName.Clone();
                result.Add(temp);
            }
        }

        return result;
    }

    private int getRandomCount()
    {
        return Random.Range(m_MinCount, m_MaxCount + 1);
    }

    private bool WillDrop()
    {
        return Random.Range(0f, 1f) <= m_Probability;
    }
}
