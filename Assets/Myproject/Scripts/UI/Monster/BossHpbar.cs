using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHpbar : MonoBehaviour
{
    [SerializeField]
    Slider hpbarPrefab;
    Slider hpbar;
    Monster monster;
    void Start()
    {
        monster = GetComponent<Monster>();
        hpbar = Instantiate(hpbarPrefab, GameManager.current.bossHpbarTrans);
    }
    private void Update()
    {
        if (hpbar != null)
        {
            hpbar.value = monster.curHp / monster.maxHp;
        }
    }
}
