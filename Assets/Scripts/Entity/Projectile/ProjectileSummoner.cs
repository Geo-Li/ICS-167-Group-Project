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

    // The projectile prefab object to make prototypes with
    [SerializeField]
    private Projectile m_ProjectilePrefab;

    // Determines how the projectile, relative to the owner, will be summoned with rotational and traslational components 
    [SerializeField]
    private SummonPosition m_SummonPositionState;

    // The 3D coordinates of the projectile's summon location
    [SerializeField]
    private Vector3 m_SummonPosition;

    // The eulerAngles of the projectile's summon rotation
    [SerializeField]
    private Vector3 m_SummonRotation;

    // The starting life time of the projectile
    [SerializeField]
    private float m_LifeTime;

    // The magnitude of the force that moves the projectile
    [SerializeField]
    private float m_PushForce;

    // Determines if the projecile moves with an initial force or with a constant force
    [SerializeField]
    private bool m_HasConstantForce;

    // Main projectile summon method that accounts for the summon state
    public Projectile ProjectileAttack(GameObject owner, Vector3 targetPosition)
    {
        Projectile result = null;

        Quaternion rotation = Quaternion.Euler(m_SummonRotation);

        switch (m_SummonPositionState)
        {
            case SummonPosition.DIRECTIONAL:
                result = ShootProjectileFromFront(m_ProjectilePrefab, m_SummonPosition, owner, m_LifeTime, m_PushForce, m_HasConstantForce);
                break;

            case SummonPosition.TARGETING:
                result = ShootProjectileTowardsTarget(m_ProjectilePrefab, m_SummonPosition, targetPosition, owner, m_LifeTime, m_PushForce, m_HasConstantForce);
                break;

            case SummonPosition.LOCAL:
                result = ShootProjectile(m_ProjectilePrefab, m_SummonPosition, rotation, owner, m_LifeTime, m_PushForce, m_HasConstantForce);
                break;

            case SummonPosition.WORLD:
                result = SummonProjectile(m_ProjectilePrefab, m_SummonPosition, rotation, owner, m_LifeTime, m_PushForce, m_HasConstantForce);
                break;
        }

        return result;
    }

    // Summons the projectile prefab in front of the owner
    public static Projectile ShootProjectileFromFront(Projectile projectilePrefab, Vector3 distanceFromOwner, GameObject owner,
                                                float lifeTime, float pushForce, bool hasConstantForce)
    {
        return DirectionalShooting(projectilePrefab, distanceFromOwner, owner.transform.rotation, owner, lifeTime, pushForce, hasConstantForce);
    }

    // Summons the projectile prefab targeting a certain position from the owner
    public static Projectile ShootProjectileTowardsTarget(Projectile projectilePrefab, Vector3 distanceFromOwner, Vector3 targetPosition, GameObject owner,
                                                        float lifeTime, float pushForce, bool hasConstantForce)
    {
        return DirectionalShooting(projectilePrefab, distanceFromOwner, Quaternion.LookRotation(targetPosition - owner.transform.position), owner, lifeTime, pushForce, hasConstantForce);
    }

    // Summons the projectile prefab relative to its initial rotation and displacement
    private static Projectile DirectionalShooting(Projectile projectilePrefab, Vector3 distanceFromOwner, Quaternion rotation, GameObject owner,
                                            float lifeTime, float pushForce, bool hasConstantForce)
    {
        Vector3 displacement = rotation * distanceFromOwner;

        return ShootProjectile(projectilePrefab, displacement, rotation, owner, lifeTime, pushForce, hasConstantForce);
    }

    // Summons the projectile prefab with a rotation and displacement from the owner
    public static Projectile ShootProjectile(Projectile projectilePrefab, Vector3 displacement, Quaternion rotation, GameObject owner,
                                        float lifeTime, float pushForce, bool hasConstantForce)
    {
        Vector3 pos = owner.transform.position + displacement;

        return SummonProjectile(projectilePrefab, pos, rotation, owner, lifeTime, pushForce, hasConstantForce);
    }

    // Summons the projectile prefab with a rotation and a position in the world
    public static Projectile SummonProjectile(Projectile projectilePrefab, Vector3 position, Quaternion rotation, GameObject owner, 
                                        float lifeTime, float pushForce, bool hasConstantForce)
    {
        Projectile instance = GameObject.Instantiate(projectilePrefab, position, rotation);
        
        instance.ChangeOwner(owner);
        instance.LifeTime = lifeTime;
        instance.PushForce = pushForce;
        instance.HasConstantForce = hasConstantForce;

        return instance;
    }
}
