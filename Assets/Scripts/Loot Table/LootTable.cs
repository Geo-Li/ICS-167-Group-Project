using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * A list of possible types of collectibles to be dropped, 
 * each with a range of possible amounts and a chance of appearing in the loot
 */

[CreateAssetMenu]
public class LootTable : ScriptableObject, LootInterface
{
    // The list of loot drops
    [SerializeField]
    private List<LootDropScript> m_Items;

    // The location where all the collectibles will spawn
    private Vector3 m_DropPosition;

    // Displacement from the original loot spawn as a Vector3
    [SerializeField]
    private Vector3 m_LootDropDisplacement = new Vector3(0, .25f, 0); 
        
    // Force of collectibles when drop
    [SerializeField]
    private float m_LootForce = 100f;

    // Public version of m_DropPosition
    public Vector3 DropPosition
    {
        get
        {
            return m_DropPosition;
        }
        set
        {
            m_DropPosition = value;
        }
    }

    // Generates a list of the randomly generated loot for each roll
    public List<GameObject> GenerateLootDrops()
    {
        List<GameObject> result = new List<GameObject>();

        foreach (LootDropScript item in m_Items)
        {
            List<GameObject> temp = item.GenerateLootDrops();

            if (temp != null)
                result.AddRange(temp);
        }

        if (result.Count == 0)
            result = null;
        else
        {
            foreach (GameObject obj in result)
            {
                obj.transform.position = m_DropPosition;
                obj.transform.position += m_LootDropDisplacement;

                Rigidbody rb = obj.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    Vector3 kVector = (Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward).normalized;

                    obj.transform.position += kVector * .5f;

                    kVector *= m_LootForce;
                    kVector.y = m_LootForce;

                    rb.AddForce(kVector);
                }
            }
        }

        return result;
    }

    // Writes the names of all the random loot for each roll in console
    public void DisplayLootDrops()
    {
        string result = "";

        List<GameObject> list = GenerateLootDrops();

        if (list == null)
            result = "No Items";
        else
        {
            foreach (GameObject item in list)
            {
                string itemName = item.name;
                result += " [ " + item + " ], ";
            }
        }

        Debug.Log(result);
    }
}
