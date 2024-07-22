using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpTable : MonoBehaviour
{
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
        text.text = string.Empty;
        for (int i = 0; i < 14; i++) 
        {
            text.text += (i + 1).ToString() + "·¾: " + (Player.current.basicExp * Mathf.Pow(1.1f, i)).ToString() + "\n";
        }
    }
}
