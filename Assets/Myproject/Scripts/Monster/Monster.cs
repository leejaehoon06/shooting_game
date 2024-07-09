using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IHittable
{
    [SerializeField]
    float _maxHp = 100;
    public float maxHp { get { return _maxHp; } set { _maxHp = value; } }
    float _curHp = 100;
    public float curHp { get { return _curHp; } set { _curHp = value; } }
    [SerializeField]
    int scorePoint;

    Animator anim;
    BoxCollider2D coll;
    Camera mianCamera;

    bool godMode = false;

    void Start()
    {
        _curHp = _maxHp;
        anim = GetComponent<Animator>();
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        mianCamera = Camera.main;
    }

    void Update()
    {
        if (transform.position.y <= mianCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y 
            && godMode == false)
        {
            godMode = true;
            coll.enabled = true;
        }
        else if (transform.position.y <= mianCamera.ScreenToWorldPoint(new Vector2(0, 0)).y - 1) 
        {
            gameObject.SetActive(false);
        }
    }

    public void TakeDamaged(float damage)
    {
        _curHp -= damage;
        if (_curHp > 0)
        {
            anim.Play("Hurt", -1, 0);
        }
        else
        {
            coll.enabled = false;
            anim.Play("Death");
            GameManager.current.AddScore(scorePoint);
            StartCoroutine(deathCheck());
        }
    }
    IEnumerator deathCheck()
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        yield return waitFrame;
        while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
        {
            yield return waitFrame;
        }
        gameObject.SetActive(false);
    }
}
