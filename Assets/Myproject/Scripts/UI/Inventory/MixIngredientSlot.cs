using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixIngredientSlot : MonoBehaviour
{
    Image image;
    [SerializeField]
    int imageSize;

    [SerializeField]
    Ingredient _ingredient;
    public Ingredient ingredient { 
        get 
        {  
            return _ingredient; 
        } 
        set 
        { 
            _ingredient = value;
            if (ingredient != null)
            {
                image.sprite = _ingredient.ingredientImage;
                UpdateSlot();
            }
            else
            {
                image.sprite = null;
            }
        } 
    }
    private void Start()
    {
        image = GetComponent<Image>();
    }
    public void OnClick()
    {
        if (LevelUpInventoryUI.current.clickIngredient != null &&
            LevelUpInventoryUI.current.clickIngredientNum > 0 && ingredient == null)
        {
            ingredient = LevelUpInventoryUI.current.clickIngredient;
            LevelUpInventoryUI.current.clickIngredientNum--;
            LevelUpInventoryUI.current.SlotReset();
        }
        else if (ingredient != null)
        {
            LevelUpInventoryUI.current.clickIngredient = ingredient;
            LevelUpInventoryUI.current.clickIngredientNum++;
            LevelUpInventoryUI.current.SlotReset();
            ImageReset();
            ingredient = null;
        }
    }
    public void ImageReset()
    {
        image.rectTransform.sizeDelta = new Vector2(imageSize, imageSize);
    }
    void UpdateSlot()
    {
        image.SetNativeSize();
        if (image.rectTransform.sizeDelta.x > image.rectTransform.sizeDelta.y)
        {
            image.rectTransform.sizeDelta = new Vector2(imageSize,
                image.rectTransform.sizeDelta.y
                * (imageSize / image.rectTransform.sizeDelta.x));
        }
        else
        {
            image.rectTransform.sizeDelta = new Vector2(
                image.rectTransform.sizeDelta.x
                * (imageSize / image.rectTransform.sizeDelta.y),
                imageSize);
        }
    }
}
