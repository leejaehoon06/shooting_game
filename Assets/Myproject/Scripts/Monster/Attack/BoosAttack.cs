using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosAttack : MonsterAttack
{
    Monster monster;
    private void Start()
    {
        monster = GetComponent<Monster>();
    }
    public override void Shot()
    {
        int rand = 0;
        if (monster.curHp / monster.maxHp >= 0.5f)
        {
            rand = Random.Range(0, 2);
        }
        else
        {
            rand = Random.Range(0, 3);
        }
        if (rand == 0)
        {
            float angle = -45f;
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(bullet, shotPos[0].transform.position, shotPos[0].transform.rotation);
                obj.transform.rotation = Quaternion.Euler(0, 0, angle + obj.transform.rotation.eulerAngles.z);
                obj.GetComponent<Bullet>().bulletDamage = damage;
                angle += 9;
            }
        }
        else if (rand == 1)
        {
            float angle = 45f;
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(bullet, shotPos[0].transform.position, shotPos[0].transform.rotation);
                obj.transform.rotation = Quaternion.Euler(0, 0, angle + obj.transform.rotation.eulerAngles.z);
                obj.GetComponent<Bullet>().bulletDamage = damage;
                angle -= 9;
            }
        }
        else
        {
            float angle = 0f;
            for (int i = 0; i < 10; i++)
            {
                GameObject obj = Instantiate(bullet, shotPos[0].transform.position, shotPos[0].transform.rotation);
                obj.transform.rotation = Quaternion.Euler(0, 0, angle + obj.transform.rotation.eulerAngles.z);
                obj.GetComponent<Bullet>().bulletDamage = damage;
                if (i % 2 == 1)
                {
                    angle = (angle + 9) * -1;
                }
                else
                {
                    angle *= -1;
                }
            }
        }
    }
}
