using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateWeapon : Weapon
{
    [SerializeField]
    Bullet[] bullets;
    public override bool Shoot()
    {
        if (base.Shoot() == false)
        {
            return false;
        }
        for (int i = 0; i < shotPosTrans.Count; i++)
        {
            int rand = Random.Range(0, bullets.Length);
            Bullet bullet = Instantiate(bullets[rand], shotPosTrans[i].transform.position, 
                shotPosTrans[i].transform.rotation);
            bullet.bulletDamage = _weaponInfo.weaponLevelInfos[0].weaponDamage;
        }
        return true;
    }
}
