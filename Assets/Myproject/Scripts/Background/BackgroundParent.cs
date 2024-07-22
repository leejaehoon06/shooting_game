using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BackgroundParent : MonoBehaviour
{
    [SerializeField]
    GameObject[] backgroundPrefabs;
    GameObject[] backgroundObjs;
    BoxCollider2D[] backgroundsCollider;
    int backgroundIndex = 0;
    float backgroundsDistance = 0;
    void Start()
    {
        backgroundObjs = new GameObject[backgroundPrefabs.Length];
        backgroundsCollider = new BoxCollider2D[backgroundPrefabs.Length];
        for (int i = 0; i < backgroundPrefabs.Length; i++)
        {
            backgroundObjs[i] = Instantiate(backgroundPrefabs[i], transform);
            backgroundsCollider[i] = backgroundObjs[i].GetComponent<BoxCollider2D>();
            backgroundsDistance += backgroundsCollider[i].bounds.size.y;
            for (int j = 0; j < i; j++)
            {
                backgroundObjs[i].transform.position += new Vector3(0, 
                    backgroundsCollider[j].bounds.size.y, 0);
            }
        }
    }
    public void BackgroundForward()
    {
        backgroundObjs[backgroundIndex].transform.position += new Vector3(0, backgroundsDistance, 0);
        backgroundIndex++;
        if(backgroundIndex >= backgroundObjs.Length)
        {
            backgroundIndex = 0;
        }
    }
    public void DestroyBackground()
    {
        for (int i = 0; i < backgroundObjs.Length; i++)
        {
            Destroy(backgroundObjs[i]);
        }
    }
}
