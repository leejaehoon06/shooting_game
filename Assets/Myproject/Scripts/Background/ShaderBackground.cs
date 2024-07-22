using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderBackground : MonoBehaviour
{
    MeshRenderer render;
    void Start()
    {
        render = GetComponent<MeshRenderer>();
        transform.parent = Camera.main.transform;
    }
    [SerializeField]
    float speed = 0.3f;
    void Update()
    {
        render.material.SetTextureOffset("_MainTex", new Vector2(0, Time.time * speed));
    }
}
