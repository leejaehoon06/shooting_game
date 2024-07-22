using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Monster[] monsters = FindObjectsOfType<Monster>();
            Bullet[] bullets = FindObjectsOfType<Bullet>();
            Vector2 minPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 maxPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            for (int i = 0; i < monsters.Length; i++)
            {
                if (monsters[i].transform.position.x >= minPos.x && monsters[i].transform.position.x <= maxPos.x &&
                    monsters[i].transform.position.y >= minPos.y && monsters[i].transform.position.y <= maxPos.y &&
                    monsters[i].monsterType != MonsterType.Boss)
                {
                    monsters[i].TakeDamaged(monsters[i].curHp);
                }
            }
            for (int i = 0; i < bullets.Length; i++)
            {
                if (bullets[i].gameObject.layer == LayerMask.NameToLayer("MonsterAttack"))
                {
                    if (bullets[i].transform.position.x >= minPos.x && bullets[i].transform.position.x <= maxPos.x &&
                    bullets[i].transform.position.y >= minPos.y && bullets[i].transform.position.y <= maxPos.y)
                    {
                        bullets[i].gameObject.SetActive(false);
                    }
                }
            }
            gameObject.SetActive(false);
        }
    }
}
