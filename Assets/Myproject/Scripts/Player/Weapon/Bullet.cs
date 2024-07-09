using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletDamage { get; set; }
    [SerializeField]
    float bulletSpeed = 3;

    void Update()
    {
        //transform.position += transform.up * Time.deltaTime; //같은 결과
        transform.Translate(bulletSpeed * Time.deltaTime, 0f,  0f, Space.Self);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Monster"))
        {
            IHittable hittable = collision.GetComponent<IHittable>();
            if (hittable != null)
            {
                hittable.TakeDamaged(bulletDamage + Player.current.attackPower);
                gameObject.SetActive(false);
            }
        }
    }
}
