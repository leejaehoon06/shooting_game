using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    WeaponInfo weaponInfo;
    public int weaponLevel { get; set; }
    public float shotTimer;


    List<GameObject> bulletObjs = new List<GameObject>();

    GameObject shotPosParent;
    List<Transform> shotPosTrans = new List<Transform>();

    public virtual void Start()
    {
        ShotPosInstan();
    }
    public void WeaponLevelUp()
    {
        weaponLevel++;
        ShotPosInstan();
    }
    public void ShotPosInstan()
    {
        if (shotPosParent != null)
        {
            Destroy(shotPosParent);
        }
        shotPosParent = Instantiate(weaponInfo.weaponLevelInfos[weaponLevel].shotPosParentPrefab, transform);
        Transform[] shotPosTransArray = shotPosParent.GetComponentsInChildren<Transform>();
        shotPosTrans = new List<Transform>();
        for (int i = 0; i < shotPosTransArray.Length; i++)
        {
            if (shotPosTransArray[i] != shotPosParent.transform)
            {
                shotPosTrans.Add(shotPosTransArray[i]);
            }
        }
    }
    public virtual void Shoot()
    {
        if(shotTimer <= weaponInfo.weaponLevelInfos[weaponLevel].shotDelay)
        {
            return;
        }
        for(int i=0; i < shotPosTrans.Count; i++)
        {
            bulletObjs.Add(Instantiate(weaponInfo.weaponLevelInfos[weaponLevel].bulletPrefab,
            shotPosTrans[i].position, shotPosTrans[i].rotation));
            bulletObjs[bulletObjs.Count - 1].GetComponent<Bullet>().bulletDamage
                = weaponInfo.weaponLevelInfos[weaponLevel].weaponDamage;
        }
        shotTimer = 0;
    }
    public virtual void UpdateTimer()
    {
        shotTimer += Time.deltaTime;
    }
}
