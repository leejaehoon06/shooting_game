using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    BackgroundParent parent;
    void Start()
    {
        parent = GetComponentInParent<BackgroundParent>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Scroll"))
        {
            parent.BackgroundForward();
        }
    }
}
