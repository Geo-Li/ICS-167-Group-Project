using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min
[CreateAssetMenu]
public class LootTable : ScriptableObject, LootInterface
{
    [SerializeField]
    private List<LootDropScript> Items;

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
