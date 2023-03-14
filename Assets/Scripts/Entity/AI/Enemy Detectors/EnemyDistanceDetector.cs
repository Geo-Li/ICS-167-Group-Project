using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Detector based on distance between target
 */
public class EnemyDistanceDetector : EnemyDetector
{
    private string m_TargetTag;
    private float m_DetectionDistance;

    // Sets the name of the tag for the player and the detection distance of the detector
    public EnemyDistanceDetector(EnemyMovement enemyAI, bool willFindTarget, string targetTag, float detectionDistance) : base(enemyAI, willFindTarget)
    {
        m_TargetTag = targetTag;
        m_DetectionDistance = detectionDistance;
    }

    public override void DoDetection()
    {
        GameObject target = m_EnemyAI.Target;

        if (m_WillFindTarget && target == null)
        {
            GameObject[] list = GameObject.FindGameObjectsWithTag(m_TargetTag);

            GameObject newTarget = null;
            int index = 0;

            while (newTarget == null && index < list.Length)
            {
                GameObject temp = list[index];

                if (m_EnemyAI.DistanceFromObject(list[index]) < m_DetectionDistance)
                    newTarget = temp;

                index++;
            }

            m_EnemyAI.Target = newTarget;
        }
        else if (target != null && m_EnemyAI.DistanceFromObject(target) >= m_DetectionDistance)
            m_EnemyAI.Target = null;
    }
}
