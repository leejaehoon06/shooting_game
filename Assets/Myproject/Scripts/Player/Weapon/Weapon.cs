using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField]
    WeaponInfo weaponInfo;
    [SerializeField]
    AudioClip audioClip;
    public AudioClip _audioClip {  get { return audioClip; } }
    public WeaponInfo _weaponInfo { get { return weaponInfo; } set { weaponInfo = value; } }
    public int weaponLevel { get; set; }
    float shotTimer;


    public List<GameObject> bulletObjs { get; set; } = new List<GameObject>();

    GameObject shotPosParent;
    public List<Transform> shotPosTrans { get; set; } = new List<Transform>();

    public virtual void Start()
    {
        ShotPosInstan();
    }
    public void WeaponLevelUp()
    {
        weaponLevel++;
        if (weaponLevel >= weaponInfo.weaponLevelInfos.Length - 1)
        {
            weaponLevel = weaponInfo.weaponLevelInfos.Length - 1;
        }
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
    public virtual bool Shoot()
    {
        if(shotTimer <= weaponInfo.weaponLevelInfos[weaponLevel].shotDelay)
        {
            return false;
        }
        shotTimer = 0;
        Player.current.audioSource.Play();
        return true;
    }
    public virtual void UpdateTimer()
    {
        shotTimer += Time.deltaTime * Player.current.attackSpeed;
    }
}
