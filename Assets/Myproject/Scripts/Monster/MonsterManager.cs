using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Red_Slime,
    Blue_Slime,
    Green_Slime,
    Black_Worm,
    Red_Worm,
    Black_Cerberus,
    Red_Cerberus,
    Boss
}
public class MonsterManager : MonoBehaviour
{
    public static MonsterManager current;

    [SerializeField]
    Monster[] monsterPrefabs;

    private void Awake()
    {
        current = this;
        monsterPrefabs = Resources.LoadAll<Monster>("MonsterInfo");
    }

    public Monster GetMosnter(MonsterType monsterType)
    {
        for (int i =0; i < monsterPrefabs.Length; i++)
        {
            if (monsterPrefabs[i].monsterType == monsterType)
            {
                return Instantiate(monsterPrefabs[i]);
            }
        }
        return null;
    }
}
