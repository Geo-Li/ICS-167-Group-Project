using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LootDropScript : LootInterface
{
    public string m_ItemName;

    public int m_MinCount, m_MaxCount;

    [Range(0f, 1f)]
    public float m_Probability;

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
