using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField]
    WaveScript[] waveScripts;
    WaveScript currentWave;

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
            while (currentWave.ShipsStillAlive())
            {
                yield return waitForEndOfFrame;
            }
        }
    }
}
