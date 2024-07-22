using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 3f;
    float moveX;
    float moveY;

    void Start()
    {
        moveX = Random.Range(-1.0f, 1.0f);
        moveY = Random.Range(-1.0f, 1.0f);

        while (Mathf.Abs(moveX) < 0.3f)
        {
            moveX = Random.Range(-1.0f, 1.0f);
        }

        while (Mathf.Abs(moveY) < 0.3f)
        {
            moveY = Random.Range(-1.0f, 1.0f);
        }
    }

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed * moveX, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed * moveY, Space.World);

        Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);
        if (pos.x < 0f)
        {
            pos.x = 0f;
            moveX = Random.Range(0.3f, 1.0f);
        }
        if (pos.y < 0f)
        {
            pos.y = 0f;
            moveY = Random.Range(0.3f, 1.0f);
        }
        if (pos.x > 1f)
        {
            pos.x = 1f;
            moveX = Random.Range(-1.0f, -0.3f);
        }
        if (pos.y > 1f)
        {
            pos.y = 1f;
            moveY = Random.Range(-1.0f, -0.3f);
        }
        transform.position = Camera.main.ViewportToWorldPoint(pos);
    }
}
