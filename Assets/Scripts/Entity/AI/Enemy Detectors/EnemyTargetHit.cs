using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Detector based on if the enemy was hit or has hit the target
 */
public class EnemyTargetHit : EnemyDetector
{
    private bool m_WantOffensive;
    private Entity m_Owner;

    // Sets the owner of the enemy AI and determines if this detector will draw from the list of offensive or victim entities
    public EnemyTargetHit(EnemyMovement enemyAI, bool willFindTarget, Entity owner, bool wantOffensive) : base(enemyAI, willFindTarget)
    {
        m_WantOffensive = wantOffensive;
        m_Owner = owner;
    }

    public override void DoDetection()
    {
        List<GameObject> list = m_Owner.GetEntityList(m_WantOffensive);

        int count = list.Count;

        GameObject obj = null;

        if (count >= 1)
            obj = list[count - 1];
        else
            return;

        GameObject target = m_EnemyAI.Target;

        if (target == null || target != obj)
        {
            m_EnemyAI.Target = obj;
            list.Remove(obj);
        }
    }
}
