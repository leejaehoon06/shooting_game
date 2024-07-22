using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCerberusAttack : MonsterAttack
{
    public override void Shot()
    {
        float plusAngle = -15;
        for (int i = 0; i < shotPos.Length * 2; i++)
        {
            Vector3 direction = (Player.current.transform.position - shotPos[i % shotPos.Length].transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - plusAngle;
            Bullet obj = Instantiate(bullet, shotPos[i].transform.position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<Bullet>();
            obj.bulletDamage = damage;
            plusAngle += 6;
        }
    }
}
