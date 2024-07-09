using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    [SerializeField]
    int speed;
    float rand;
    private void Start()
    {
        rand = Random.Range(speed * 0.8f, speed * 1.5f);
    }

    void Update()
    {
        transform.position += new Vector3(rand * Time.deltaTime, 0, 0);
    }
}
