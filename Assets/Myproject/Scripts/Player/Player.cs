using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
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
    RuntimeAnimatorController[] playerAnimations;
    [SerializeField]
    int[] levelUpNums;
    Animator anim;
    
    //bool DeathSwitch = false;
    Vector3 pos;

    [SerializeField]
    float speed = 3;
    [SerializeField]
    float _maxHp = 100;
    public float maxHp { get { return _maxHp; } set {  _maxHp = value; } }
    float _curHp = 100;
    public float curHp { get { return _curHp; } set { _curHp = value; } }
    [SerializeField]
    float _attackPower = 10;
    public float attackPower { get { return _attackPower; } set { _attackPower = value; } }
    public int level { get; private set; } = 1;
    public float maxExp { get; private set; } = 50;
    public float curExp { get; private set; }
    public float maxUltimatePoint { get; } = 500;
    public float curUltimatePoint { get; private set; }
    Vector2 minPos;
    Vector2 maxPos;

    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        _curHp = _maxHp;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Time.timeScale > 0)
        {
            minPos = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            maxPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
            Move();
            weapon?.UpdateTimer();
            if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
            {
                weapon.Shoot();
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
        pos += direction * Time.deltaTime * speed;
        transform.position += new Vector3(direction.x * Time.deltaTime * speed, direction.y * Time.deltaTime * speed, 0f);
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
    void LevelUp()
    {
        level++;
        curExp -= maxExp;
        maxExp *= 1.1f;
        maxHp *= 1.1f;
        curHp = maxHp;
        attackPower *= 1.1f;
        switch (level)
        {//이미지 변경
            case 5:
                anim.runtimeAnimatorController = playerAnimations[0];
                break;
            case 11:
                anim.runtimeAnimatorController = playerAnimations[1];
                break;
        }

    }
    public void PlusUltimatePoint(float UltimateNum)
    {
        curUltimatePoint += UltimateNum;
        if(curUltimatePoint >= maxUltimatePoint)
        {
            curUltimatePoint = maxUltimatePoint;
        }
    }
    public void Ultimate()
    {
        curUltimatePoint = 0;
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
