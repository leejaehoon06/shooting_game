using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    int speed;

    void Update()
    {
        transform.position += new Vector3(0, speed * Time.deltaTime, 0);
    }
}
