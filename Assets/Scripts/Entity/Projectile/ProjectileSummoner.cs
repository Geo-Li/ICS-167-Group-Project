using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// William Min

/*
 * Prototype script for summoning projectiles
 */
[CreateAssetMenu]
public class ProjectileSummoner : ScriptableObject
{
    private enum SummonPosition
    {
        DIRECTIONAL,
        TARGETING,
        LOCAL,
        WORLD
    }

    [SerializeField]
    private Projectile m_ProjectilePrefab;

    [SerializeField]
    private SummonPosition m_SummonPositionState;

    [SerializeField]
    private Vector3 m_SummonPosition;

    [SerializeField]
    private Vector3 m_SummonRotation;

    public Projectile ProjectileAttack(GameObject owner, Vector3 targetPosition)
    {
        Projectile result = null;

        Quaternion rotation = Quaternion.Euler(m_SummonRotation);

        switch (m_SummonPositionState)
        {
            case SummonPosition.DIRECTIONAL:
                result = ShootProjectileFromFront(m_ProjectilePrefab, m_SummonPosition, owner);
                break;

            case SummonPosition.TARGETING:
                result = ShootProjectileTowardsTarget(m_ProjectilePrefab, m_SummonPosition, targetPosition, owner);
                break;

            case SummonPosition.LOCAL:
                result = ShootProjectile(m_ProjectilePrefab, m_SummonPosition, rotation, owner);
                break;

            case SummonPosition.WORLD:
                result = SummonProjectile(m_ProjectilePrefab, m_SummonPosition, rotation, owner);
                break;
        }

        return result;
    }

    public Projectile ShootProjectileFromFront(Projectile projectilePrefab, Vector3 distanceFromOwner, GameObject owner)
    {
        return DirectionalShooting(projectilePrefab, distanceFromOwner, owner.transform.rotation, owner);
    }

    public Projectile ShootProjectileTowardsTarget(Projectile projectilePrefab, Vector3 distanceFromOwner, Vector3 targetPosition, GameObject owner)
    {
        return DirectionalShooting(projectilePrefab, distanceFromOwner, Quaternion.LookRotation(targetPosition - owner.transform.position), owner);
    }

    private Projectile DirectionalShooting(Projectile projectilePrefab, Vector3 distanceFromOwner, Quaternion rotation, GameObject owner)
    {
        Vector3 displacement = rotation * distanceFromOwner;

        return ShootProjectile(projectilePrefab, displacement, rotation, owner);
    }

    public Projectile ShootProjectile(Projectile projectilePrefab, Vector3 displacement, Quaternion rotation, GameObject owner)
    {
        Vector3 pos = owner.transform.position + displacement;

        return SummonProjectile(projectilePrefab, pos, rotation, owner);
    }

    public Projectile SummonProjectile(Projectile projectilePrefab, Vector3 position, Quaternion rotation, GameObject owner)
    {
        Projectile instance = GameObject.Instantiate(projectilePrefab, position, rotation);

        instance.ChangeOwner(owner);

        Debug.Log(instance.GetOwner(true));

        return instance;
    }
}
