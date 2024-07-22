using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpButton : MonoBehaviour
{
    [SerializeField]
    GameObject helpObj;
    private void Start()
    {
        helpObj.SetActive(false);
    }
    private void Update()
    {
        if (helpObj.activeSelf && Input.anyKeyDown)
        {
            helpObj.SetActive(false);
        }
    }
    public void OnClick()
    {
        helpObj.SetActive(true);
    }
}
