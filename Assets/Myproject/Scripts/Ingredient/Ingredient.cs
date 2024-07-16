using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ingredient : ScriptableObject
{
    public Sprite ingredientImage;
    public string ingredientName;
    [TextArea]
    public string ingredientInfo;
}
