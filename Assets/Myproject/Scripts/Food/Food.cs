using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Food : ScriptableObject
{
    public Sprite foodImage;
    public string foodName;
    [TextArea]
    public string foodInfo;
}
