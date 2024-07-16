using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public static IngredientManager current;

    [SerializeField]
    IngredientObj[] ingredientObj;

    private void Awake()
    {
        current = this;
    }

    public IngredientObj GetIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < ingredientObj.Length; i++)
        {
            if (ingredientObj[i].ingredient == ingredient)
            {
                return Instantiate(ingredientObj[i]);
            }
        }
        return null;
    }
}
