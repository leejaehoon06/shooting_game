using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [SerializeField]
    float instanTime;
    [SerializeField]
    GameObject[] CloudPrefabs;
    List<GameObject> CloudObjs = new List<GameObject>();

    float instanTimer;
    //오브젝트 풀링 해야함
    void Update()
    {
        instanTimer += Time.deltaTime;
        if (instanTimer >= instanTime)
        {
            int rand = Random.Range(0, CloudPrefabs.Length);
            CloudObjs.Add(Instantiate(CloudPrefabs[rand], transform));
            Vector3 vector3 = Camera.main.ScreenToWorldPoint(new Vector2(0, 
                Random.Range(Screen.height * 0.3f, Screen.height * 1.5f)));
            CloudObjs[CloudObjs.Count - 1].transform.position = vector3 - new Vector3(1, 0, 0);
            instanTimer = 0;
        }
    }
}
