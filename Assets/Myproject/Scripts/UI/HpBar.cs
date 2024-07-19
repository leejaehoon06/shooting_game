using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    Camera mainCamera;
    Transform playerTrans;
    void Start()
    {
        mainCamera = Camera.main;
        playerTrans = Player.current.HpPoint;
    }

    
    void Update()
    {
        transform.position = mainCamera.WorldToScreenPoint(playerTrans.position);
    }
}
