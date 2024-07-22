using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixManager : MonoBehaviour
{
    [SerializeField]
    MixIngredientSlot[] mixIngredientSlots;
    [SerializeField]
    Food[] foods;
    [SerializeField]
    Food trash;
    [SerializeField]
    FoodImport foodImprot;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void MixButtonClick()
    {
        List<Ingredient> ingredients = new List<Ingredient>();
        for (int i = 0; i < mixIngredientSlots.Length; i++)
        {
            if (mixIngredientSlots[i].ingredient == null)
            {
                return;
            }
            else
            {
                ingredients.Add(mixIngredientSlots[i].ingredient);
            }
        }
        for (int i = 0; i < foods.Length; i++)
        {
            List<Ingredient> recipe = new List<Ingredient>();
            for (int j = 0; j < foods[i].foodRecipe.Count; j++)
            {
                recipe.Add(foods[i].foodRecipe[j]);
            }
            for (int j = 0; j < ingredients.Count; j++)
            {
                if (recipe.IndexOf(ingredients[j]) >= 0)
                {
                    recipe.Remove(ingredients[j]);
                }
            }
            if (recipe.Count == 0)
            {
                MixFood(foods[i]);
                MinIngredient(ingredients.ToArray());
                audioSource.Play();
                return;
            }
        }
        MixFood(trash);
        MinIngredient(ingredients.ToArray());
    }
    void MixFood(Food food)
    {
        foodImprot.food = food;
        foodImprot.gameObject.SetActive(true);
        foodImprot.UpdateImport();
        for (int i = 0; i < food.buff.Length; i++)
        {
            FoodBuffManager.current.Buff(food.buff[i].buffType, food.buff[i].buffNum, food.buff[i].buffTime);
        }
    }
    void MinIngredient(Ingredient[] ingredients)
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            InventoryUI.current.MinIngredient(ingredients[i]);
            mixIngredientSlots[i].ingredient = null;
            mixIngredientSlots[i].ImageReset();
        }
        LevelUpInventoryUI.current.SlotReset();
        LevelUpInventoryUI.current.InventoryFreshSlot();
    }
}
