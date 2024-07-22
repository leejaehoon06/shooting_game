using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IHittable
{
    public static Player current;
    [SerializeField]
    Transform _HpPoint;
    public Transform HpPoint {  get { return _HpPoint; } }
    [SerializeField] 
    Weapon _weapon;
    public Weapon weapon { get { return _weapon; } set { _weapon = value; } }
    public Dictionary<Ingredient, int> inventory = new Dictionary<Ingredient, int>();

    [SerializeField]
    Weapon ultimateWeapon;
    Weapon beforeWeapon;

    [SerializeField]
    RuntimeAnimatorController[] playerAnimations;
    [SerializeField]
    int[] levelUpNums;
    Animator anim;
    Collider2D coll;
    public AudioSource audioSource { get; set; }
    
    Vector3 pos;

    [SerializeField]
    float _speed = 5;
    public float speed { get { return _speed; } set { _speed = value; } }
    [SerializeField]
    float _maxHp = 100;
    public float maxHp { get { return _maxHp; } set {  _maxHp = value; } }
    float _curHp = 100;
    public float curHp { get { return _curHp; } set { _curHp = value; } }
    [SerializeField]
    float _attackPower = 1;
    public float attackPower { get { return _attackPower; } set { _attackPower = value; } }
    public float attackSpeed { get; set; } = 1f;
    public int level { get; private set; } = 1;
    public float basicExp { get; private set; }
    public float maxExp { get; private set; } = 50;
    public float curExp { get; private set; }
    public float maxUltimatePoint { get; } = 500;
    public float curUltimatePoint { get; private set; }
    public bool godMod { get; set; } = false;
    Vector2 minPos;
    Vector2 maxPos;

    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        _curHp = _maxHp;
        basicExp = maxExp;
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = weapon._audioClip;
        ultimateWeapon.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Time.timeScale > 0)
        {
            minPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            maxPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            Move();
            weapon?.UpdateTimer();
            if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                weapon.Shoot();
            }
            if (Input.GetKeyDown(KeyCode.Q) && curUltimatePoint >= maxUltimatePoint)
            {
                StartCoroutine(Ultimate());
            }
        }
    }
    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(x, y, 0f);
        direction.Normalize();
        pos = transform.position;
        //transform.Translate(direction * Time.deltaTime * speed, Space.World);
        pos += direction * Time.deltaTime * _speed;
        transform.position += new Vector3(direction.x * Time.deltaTime * _speed, direction.y * Time.deltaTime * _speed, 0f);
        pos.x = Mathf.Clamp(pos.x, minPos.x, maxPos.x);
        pos.y = Mathf.Clamp(pos.y, minPos.y, maxPos.y);
        transform.position = pos;
    }
    public void PlusExp(float ExpNum)
    {
        curExp += ExpNum;
        if(curExp >= maxExp)
        {
            LevelUp();
        }
    }
    public void LevelUp()
    {
        level++;
        curExp -= maxExp;
        maxExp *= 1.1f;
        maxHp *= 1.1f;
        curHp = maxHp;
        attackPower += 1.1f;
        for (int i = 0; i < levelUpNums.Length; i++)
        {
            if (levelUpNums[i] == level)
            {
                anim.runtimeAnimatorController = playerAnimations[i];
                break;
            }
        }
        LevelUpInventoryUI.current.LevelUp();
    }
    public void LevelDown()
    {
        level--;
        if (level <= 0)
        {
            level = 1;
        }
        maxExp /= 1.1f;
        maxHp /= 1.1f;
        curHp = maxHp;
        attackPower -= 1.1f;
    }
    public void PlusUltimatePoint(float UltimateNum)
    {
        curUltimatePoint += UltimateNum;
        if(curUltimatePoint >= maxUltimatePoint)
        {
            curUltimatePoint = maxUltimatePoint;
        }
    }
    public IEnumerator Ultimate()
    {
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        beforeWeapon = weapon;
        weapon.gameObject.SetActive(false);
        weapon = ultimateWeapon;
        weapon.gameObject.SetActive(true);
        audioSource.clip = weapon._audioClip;
        while (curUltimatePoint > 0)
        {
            curUltimatePoint -= 50f * Time.deltaTime;
            yield return waitForEndOfFrame;
        }
        weapon.gameObject.SetActive(false);
        weapon = beforeWeapon;
        weapon.gameObject.SetActive(true);
        audioSource.clip = weapon._audioClip;
        curUltimatePoint = 0;
    }
    public void GetWeapon(Weapon weapon)
    {
        if (weapon._weaponInfo != _weapon._weaponInfo)
        {
            if (_weapon == ultimateWeapon)
            {
                Destroy(beforeWeapon.gameObject);
                beforeWeapon = Instantiate(weapon, transform.position, transform.rotation);
                beforeWeapon.transform.parent = transform;
                beforeWeapon.gameObject.SetActive(false);
            }
            else
            {
                Destroy(_weapon.gameObject);
                _weapon = Instantiate(weapon, transform.position, transform.rotation);
                _weapon.transform.parent = transform;
                audioSource.clip = _weapon._audioClip;
            }
        }
        else
        {
            _weapon.WeaponLevelUp();
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

        }
        else
        {
            coll.enabled = false;
            Time.timeScale = 0;
            GameManager.current.Died();
        }
    }
    public void Heal(float num)
    {
        curHp += num;
        if (curHp >= maxHp)
        {
            curHp = maxHp;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IngredientObj>() != null)
        {
            IngredientObj ingredient = collision.gameObject.GetComponent<IngredientObj>();
            if (inventory.ContainsKey(ingredient.ingredient))
            {
                inventory[ingredient.ingredient]++;
                InventoryUI.current.AddIngredientNum(ingredient.ingredient);
            }
            else
            {
                inventory.Add(ingredient.ingredient, 1);
                InventoryUI.current.AddInventorySlot(ingredient.ingredient);
            }
            collision.gameObject.SetActive(false);
        }
    }
}
