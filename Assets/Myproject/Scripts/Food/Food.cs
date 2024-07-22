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
    public List<Ingredient> foodRecipe;
    public Buff[] buff;
}
[System.Serializable]
public class Buff
{
    public BuffType buffType;
    public float buffNum;
    public float buffTime;
}