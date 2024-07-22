using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCerberusAttack : MonsterAttack
{
    public override void Shot()
    {
        for (int i = 0; i < shotPos.Length; i++)
        {
            Vector3 direction = (Player.current.transform.position - shotPos[i].transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Bullet obj = Instantiate(bullet, shotPos[i].transform.position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<Bullet>();
            obj.bulletDamage = damage;
        }
    }
}
