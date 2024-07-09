using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveScript : MonoBehaviour
{
    [SerializeField]
    GameObject[] monsterParent;
    List<List<GameObject>> monsterChild = new List<List<GameObject>>();
    private void Awake()
    {
        for(int i = 0; i < monsterParent.Length; i++)
        {
            monsterChild.Add(new List<GameObject>());
            if (i <= GameManager.current.difficulty)
            {
                for (int j = 0; j < monsterParent[i].transform.childCount; j++)
                {
                    monsterChild[i].Add(monsterParent[i].transform.GetChild(j).gameObject);
                }
            }
            else
            {
                for (int j = 0; j < monsterParent[i].transform.childCount; j++)
                {
                    monsterParent[i].transform.GetChild(j).gameObject.SetActive(false);
                }
            }
        }
    }

    public bool ShipsStillAlive()
    { 
        for (int i = 0; i < monsterChild.Count; i++)
        {
            for (int j = 0; j < monsterChild[i].Count; j++)
            {
                if (monsterChild[i][j].activeSelf == true)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
