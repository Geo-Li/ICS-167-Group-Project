using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

public class Entity : MonoBehaviour
{
    [SerializeField]
    private HealthScript m_HealthBar;

    private AnimationManager m_EntityManager;

    [SerializeField]
    private bool m_Dead = false;

    [SerializeField]
    public bool IsDead
    {
        get 
        {
            return m_Dead;
        }
        set
        {
            m_Dead = value;
        }
    }

    void Start()
    {
        m_EntityManager = this.GetComponent<AnimationManager>();

        if (m_EntityManager == null)
            Debug.LogErrorFormat("An Entity component is not on an Entity.");
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDead)
            Destroy(this.gameObject);
    }

    public void performAttack(int attackNumber)
    {
        m_EntityManager.ActionState.ParameterValue = attackNumber;
    }
}
