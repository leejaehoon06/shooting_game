using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTimer : MonoBehaviour
{
    [SerializeField]
    int timer = 1;
    void Start()
    {
        Invoke("Disable", timer);
    }
    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
