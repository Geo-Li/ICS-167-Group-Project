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
    private List<LootDropScript> Items;

    // Generates a list of the randomly generated loot for each roll
    public List<string> GenerateLootDrops()
    {
        List<string> result = new List<string>();

        foreach (LootDropScript item in Items)
        {
            List<string> temp = item.GenerateLootDrops();

            if (temp != null)
                result.AddRange(temp);
        }

        if (result.Count == 0)
            result = null;

        return result;
    }

    // Writes the names of all the random loot for each roll in console
    public void DisplayLootDrops()
    {
        string result = "";

        List<string> list = GenerateLootDrops();

        if (list == null)
            result = "No Items";
        else
        {
            foreach (string item in list)
            {
                result += item + " ";
            }
        }

        Debug.Log(result);
    }
}
