using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveType
{
    public MonsterType[] monsterTypes;
}
public class WaveManager : MonoBehaviour
{
    [SerializeField]
    WaveScript[] waveScripts;
    WaveScript currentWave;

    [SerializeField]
    WaveType[] waveTypes;

    IEnumerator Start()
    {
        if (waveScripts.Length == 0)
        {
            yield break;
        }
        Camera camera = Camera.main;
        WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int rand = Random.Range(0, waveScripts.Length);
            GameObject obj = Instantiate(waveScripts[rand].gameObject, camera.transform.position, camera.transform.rotation);
            obj.transform.position += new Vector3(0, 10);
            currentWave = obj.GetComponent<WaveScript>();
            currentWave.monsterTypes = waveTypes[GameManager.current.difficulty].monsterTypes;
            while (currentWave.ShipsStillAlive())
            {
                yield return waitForEndOfFrame;
            }
        }
    }
}
