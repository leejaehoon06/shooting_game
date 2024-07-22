using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropObj : MonoBehaviour
{
    [SerializeField]
    WeaponInfo weaponInfo;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Player.current.GetWeapon(weaponInfo.weapon);
            gameObject.SetActive(false);
        }
    }
    //움직임 구현
}
