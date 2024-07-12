using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsterTrans;
    [SerializeField]
    List<GameObject> monsterChild = new List<GameObject>();
    public MonsterType[] monsterTypes;

    Camera mainCamera;
    private void Start()
    {
        mainCamera = Camera.main;

        for (int i = 0; i < monsterTrans[GameManager.current.difficulty].transform.childCount; i++)
        {
            monsterChild.Add(MonsterManager.current.GetMosnter(monsterTypes
                [Random.Range(0, monsterTypes.Length)]).gameObject);
            monsterChild[monsterChild.Count - 1].transform.position = 
                monsterTrans[GameManager.current.difficulty].transform.GetChild(i).transform.position;
            monsterChild[monsterChild.Count - 1].transform.parent = 
                monsterTrans[GameManager.current.difficulty].transform.GetChild(i).transform;
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
