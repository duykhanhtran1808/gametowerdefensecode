using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticle;
    [SerializeField] float fireRange = 25f;
    Transform enemyTarget;
    //void Start()
    //{
    //    enemyTarget = FindObjectOfType<Enemy>().transform;

    //}

    void Update()
    {
        FindClosetTarget();
        AimWeapon();
    }

    void FindClosetTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        enemyTarget = closestTarget;


    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, enemyTarget.position);
        weapon.LookAt(enemyTarget);

        if (targetDistance < fireRange)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = isActive;
    }
}
