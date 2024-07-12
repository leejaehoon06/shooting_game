using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsterTrans;
    List<GameObject> monsterChild = new List<GameObject>();
    public MonsterType[] monsterTypes { get; set; }

    Camera mainCamera;
    private void Awake()
    {
        mainCamera = Camera.main;

        for (int i = 0; i < GameManager.current.difficulty; i++)
        {
            for (int j = 0; j < monsterTrans[i].transform.childCount; j++)
            {
                MonsterManager.current.GetMosnter(monsterTypes[Random.Range(0, monsterTypes.Length)]);
            }
        }
    }
    
    private void OnEnable()
    {
        transform.parent = null;
    }
    private void Update()
    {
        if (transform.position.y <= mainCamera.transform.position.y && transform.parent == null)
        {
            transform.parent = mainCamera.transform;
            for (int i = 0; i < monsterChild.Count; i++)
            {
                 monsterChild[i].GetComponent<Monster>().Arrive();
            }
        }
    }
    public bool ShipsStillAlive()
    { 
        for (int i = 0; i < monsterChild.Count; i++)
        {
            if (monsterChild[i].activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }
}
