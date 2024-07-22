using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    [SerializeField]
    GameObject _bullet;
    public GameObject bullet {  get { return _bullet; } }
    [SerializeField]
    GameObject[] _shotPos;
    public GameObject[] shotPos { get { return _shotPos; } }
    [SerializeField]
    float _damage;
    public float damage { get { return _damage; } }
    [SerializeField]
    float shotDelay;
    float timer;

    
    void Update()
    {
        timer += Time.deltaTime;
        if (Time.timeScale > 0)
        {
            if (timer >= shotDelay)
            {
                timer = 0;
                Shot();
            }
        }
    }
    public virtual void Shot()
    {

    }
}
