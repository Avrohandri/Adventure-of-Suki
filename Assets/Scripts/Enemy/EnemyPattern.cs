using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPattern : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private Projectile projectilePrefab;
    [SerializeField] private float damage;

    public Projectile GetProjectile()
    {
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.Damage = damage;
        projectile.Direction = Vector3.right;
        return projectile;
    }
}
