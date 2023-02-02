using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Indicates that a class can generate loot drops
 */
public interface LootInterface
{
    // Generates loot drops
    public List<string> GenerateLootDrops();
}
