using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttack : MonsterAttack
{
    public override void Shot()
    {
        Vector3 direction = (Player.current.transform.position - shotPos[0].transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Bullet obj = Instantiate(bullet, shotPos[0].transform.position, Quaternion.Euler(new Vector3(0, 0, angle))).GetComponent<Bullet>();
        obj.bulletDamage = damage;
    }
}
