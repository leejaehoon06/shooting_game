using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedWeapon : Weapon
{
    public override bool Shoot()
    {
        if (base.Shoot() == false)
        {
            return false;
        }
        for (int i = 0; i < shotPosTrans.Count; i++)
        {
            bulletObjs.Add(Instantiate(_weaponInfo.weaponLevelInfos[weaponLevel].bulletPrefab,
            shotPosTrans[i].position, shotPosTrans[i].rotation));
            bulletObjs[bulletObjs.Count - 1].GetComponent<Bullet>().bulletDamage
                = _weaponInfo.weaponLevelInfos[weaponLevel].weaponDamage;
        }
        return true;
    }
}
