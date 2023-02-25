using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// William Min

/*
 * Moth AI and movemment
 */
public class Moth : Enemy
{
    [SerializeField]
    private ProjectileSummoner[] m_Summons;

    private List<Projectile> m_CurrentProjectiles = new List<Projectile>();

    // Sprites for change in animations
    [SerializeField]
    //private GameObject m_NeutralLeftEye, m_NeutralLeftEyelid, m_NeutralRightEye, m_NeutralRightEyelid,
    //m_HappyLeftEye, m_HappyLeftEyelid, m_HappyRightEye, m_HappyRightEyelid;
    private GameObject m_NeutralFace, m_HappyFace;

    protected override void Start()
    {
        DisplayGameObjectNullErrorMessage(m_NeutralFace);
        DisplayGameObjectNullErrorMessage(m_HappyFace);

        base.Start();
    }

    protected override void EntityController()
    {
        GameObject target = m_Movement.Target;
        NavMeshAgent agent = m_Movement.Agent;

        if (target == null)
        {
            m_Movement.Wander();

            m_Movement.FindTargetByDistance(m_PlayerTag, m_DetectionDistance);

            agent.speed = m_WanderingSpeed;
        }
        else
        {
            m_Movement.SeekTarget();

            if (m_Movement.DistanceFromObject(target) >= m_DetectionDistance)
                m_Movement.Target = null;

            agent.speed = m_ActiveSpeed;
        }

        UpdateProjectileList();
    }

    protected override void ExpressionMaker()
    {
        bool IsChasing = m_Movement.Target != null;

        m_NeutralFace.SetActive(!IsChasing);
        m_HappyFace.SetActive(IsChasing);
    }

    private void UpdateProjectileList()
    {
        for (int i = m_CurrentProjectiles.Count - 1; i >= 0; i--)
        {
            Projectile temp = m_CurrentProjectiles[i];

            if (temp == null)
                m_CurrentProjectiles.RemoveAt(i);
        }
    }

    public void DoProjectileAttack(int index)
    {
        if (m_Movement.Target != null)
            m_CurrentProjectiles.Add(m_Summons[index].ProjectileAttack(this.gameObject, m_Movement.Target.transform.position));
    }

    public void DoProjectileAttacks(int minIndex, int maxIndex)
    {
        if (minIndex < 0)
            Debug.LogError("");

        while (minIndex < maxIndex)
        {
            DoProjectileAttack(minIndex);
            minIndex++;
        }
    }
}
