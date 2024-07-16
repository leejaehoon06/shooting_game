using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player current;
    [SerializeField] 
    Weapon _weapon;
    public Weapon weapon { get { return _weapon; } set { _weapon = value; } }
    public Dictionary<Ingredient, int> Inventory = new Dictionary<Ingredient, int>();
    
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
    public float curExp { get; set; }

    Vector2 minPos;
    Vector2 maxPos;

    private void Awake()
    {
        current = this;
    }
    private void Start()
    {
        _curHp = _maxHp;
        curExp = maxExp;
    }
    void Update()
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
            level++;
            curExp -= maxExp;
            maxExp *= 1.1f;
        }
        //
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<IngredientObj>() != null)
        {
            IngredientObj ingredient = collision.gameObject.GetComponent<IngredientObj>();
            if (Inventory.ContainsKey(ingredient.ingredient))
            {
                Inventory[ingredient.ingredient]++;
            }
            else
            {
                Inventory.Add(ingredient.ingredient, 1);
            }
            collision.gameObject.SetActive(false);
        }
    }
}
