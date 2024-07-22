using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour, IHittable
{
    [SerializeField]
    MonsterType _monsterType;
    public MonsterType monsterType { get { return _monsterType; } }
    [SerializeField]
    Ingredient dropIngredient;

    [SerializeField]
    float hitPlayerDamage = 5;
    [SerializeField]
    float _maxHp = 100;
    public float maxHp { get { return _maxHp; } private set { _maxHp = value; } }
    float _curHp = 100;
    public float curHp { get { return _curHp; } private set { _curHp = value; } }
    [SerializeField]
    int scorePoint;

    Animator anim;
    BoxCollider2D coll;
    Camera mainCamera;
    //MonsterMove move;
    MonsterAttack attack;

    bool godMode = false;

    void Start()
    {
        attack = GetComponent<MonsterAttack>();
        attack.enabled = false;
        _curHp = _maxHp;
        if (GetComponent<Animator>() != null)
        {
            anim = GetComponent<Animator>();
        }
        coll = GetComponent<BoxCollider2D>();
        coll.enabled = false;
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Time.timeScale > 0) 
        { 
            if (transform.position.y <= mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y
            && godMode == false)
            {
                godMode = true;
                coll.enabled = true;
            }
        }
    }

    public void Arrive()
    {
        attack.enabled = true;
        coll.enabled = true;
        if (monsterType == MonsterType.Boss)
        {
            mainCamera.GetComponent<CameraMove>().enabled = false;
            GameManager.current.BossArive();
        }
    }

    public void TakeDamaged(float damage)
    {
        if (coll.enabled == false)
        {
            return;
        }
        _curHp -= damage;
        if (_curHp > 0)
        {
            if (GetComponent<Animator>() != null)
                anim.Play("Hurt", -1, 0);
        }
        else
        {
            coll.enabled = false;
            attack.enabled = false;
            if (GetComponent<Animator>() != null)
                anim.Play("Death");
            GameManager.current.AddScore(scorePoint);
            if (monsterType == MonsterType.Boss)
            {
                GameManager.current.Clear();
            }
            else
            {
                Player.current.PlusExp(scorePoint);
                Player.current.PlusUltimatePoint(scorePoint);
                DropItem();
                StartCoroutine(deathCheck());
            }
        }
    }
    void DropItem()
    {
        if (Random.Range(0, 10) < 5)
        {
            IngredientObj ingredientObj = IngredientManager.current.GetIngredient(dropIngredient);
            ingredientObj.gameObject.transform.position = transform.position;
        }
        if (Random.Range(0, 10) < 3)
        {
            GameObject obj = DropManager.current.InstanWeaponDrop();
            obj.transform.parent = mainCamera.transform;
            obj.transform.position = transform.position;
        }
        if (Random.Range(0, 10) < 2)
        {
            GameObject obj = DropManager.current.IstanNormalDrop();
            obj.transform.parent = mainCamera.transform;
            obj.transform.position = transform.position;
        }
    }
    IEnumerator deathCheck()
    {
        if (anim != null) 
        { 
            WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
            yield return waitFrame;
            while (anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
            {
                yield return waitFrame;
            }
        }
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            IHittable hittable = collision.GetComponent<IHittable>();
            if (Player.current.godMod == false)
            {
                hittable.TakeDamaged(hitPlayerDamage);
            }
        }
    }
}
