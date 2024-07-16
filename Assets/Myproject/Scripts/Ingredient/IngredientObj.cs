using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientObj : MonoBehaviour
{
    [SerializeField]
    Ingredient _ingredientInfo;
    public Ingredient ingredient { get {  return _ingredientInfo; } }
    SpriteRenderer sprite;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = _ingredientInfo.ingredientImage;
    }
}
