using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackWormAttack : MonsterAttack
{
    public override void Shot()
    {
        StartCoroutine("MultyShot");
    }
    IEnumerator MultyShot()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(0.07f);
        
        for (int i = 0; i < 4; i++)
        {
            Vector3 direction = (Player.current.transform.position - shotPos[0].transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float rand = angle - Random.Range(-15f, 15f);
            Bullet obj = Instantiate(bullet, shotPos[0].transform.position, Quaternion.Euler(new Vector3(0, 0, rand))).GetComponent<Bullet>();
            obj.bulletDamage = damage;
            yield return waitForSeconds;
        }
    }
}
