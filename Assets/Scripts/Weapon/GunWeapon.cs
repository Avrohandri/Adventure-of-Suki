﻿using UnityEngine;

public class GunWeapon : Weapon
{
    [SerializeField] private Projectile projectilePrefab;

    public override void UseWeapon()
    {
        PlayShootAnimation();

        // Create projectile
        Projectile projectile = Instantiate(projectilePrefab);
        projectile.transform.position = shootPos.position;
        projectile.Direction = shootPos.right;
        if (CharacterParent is PlayerWeapon player)
        {
            projectile.Damage = player.GetDamageUsingCricitalChance();
        }
        else
        {
            projectile.Damage = itemWeapon.Damage;
        }

        float randomSpread = Random.Range(itemWeapon.MinSpread, itemWeapon.MaxSpread);
        projectile.transform.rotation = 
            Quaternion.Euler(randomSpread * Vector3.forward);
    }

    public override void DestroyWeapon()
    {
        Destroy(gameObject);
    }
}