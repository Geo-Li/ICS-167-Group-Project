using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Scriptable Object to store item data in the inventory

[CreateAssetMenu]
public class ItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
}
