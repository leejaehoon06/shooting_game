using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player current;
    [SerializeField] 
    Weapon _weapon;
    public Weapon weapon { get { return _weapon; } set { _weapon = value; } }

    
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
    int level = 0;
    int maxExp = 5;
    int curExp = 0;

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

}
