using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class WeaponInfo : ScriptableObject
{
    public Weapon weapon;
    public WeaponLevelInfo[] weaponLevelInfos;
}
[System.Serializable]
public class WeaponLevelInfo
{
    public float shotDelay;
    public float weaponDamage;
    public GameObject shotPosParentPrefab;
    public GameObject bulletPrefab;
}