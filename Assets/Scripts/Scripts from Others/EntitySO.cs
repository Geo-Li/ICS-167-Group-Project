using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Entity")]
public class EntitySO : ScriptableObject
{

    [Header("Only Gameplay")]
    public float speed = 10f;
    public int health = 100;
    public float attackRange = 5f;

}
