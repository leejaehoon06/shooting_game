using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    [SerializeField]
    GameObject bossPrefab;
    GameObject boss;

    Camera mainCamera;
    void Start()
    {
        mainCamera = Camera.main;
        boss = Instantiate(bossPrefab, mainCamera.transform.position + 
            new Vector3(0, 15), mainCamera.transform.rotation);
    }
    private void Update()
    {
        
        if (boss.transform.position.y <= mainCamera.transform.position.y + 2.5f)
        {
            boss.GetComponent<Monster>().Arrive();
        }
        else
        {
            boss.transform.position -= new Vector3(0, 1.5f * Time.deltaTime, 0);
        }
    }
}
