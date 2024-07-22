using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager current;

    [SerializeField]
    WeaponDropObj[] _weaponDropObjs;
    public WeaponDropObj[] weaponDropObjs {  get { return _weaponDropObjs; } }
    [SerializeField]
    GameObject[] _normalDropObjs;
    GameObject[] normalDropObjs { get { return _normalDropObjs; } }

    private void Awake()
    {
        current = this;
    }
    public GameObject InstanWeaponDrop()
    {
        return Instantiate(weaponDropObjs[Random.Range(0, weaponDropObjs.Length)].gameObject);
    }
    public GameObject IstanNormalDrop()
    {
        return Instantiate(normalDropObjs[Random.Range(0, normalDropObjs.Length)].gameObject);
    }
}
