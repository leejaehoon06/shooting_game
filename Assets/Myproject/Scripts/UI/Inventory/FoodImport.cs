using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodImport : MonoBehaviour
{
    [SerializeField]
    Image foodImage;
    [SerializeField]
    Text foodName;
    [SerializeField]
    Text foodInfo;
    float imageSize;
    private void Start()
    {
        imageSize = foodImage.rectTransform.sizeDelta.x;
        gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            gameObject.SetActive(false);
        }
    }
    public void UpdateImport()
    {
        if (_food == null)
        {
            
        }
        else
        {
            foodImage.color = new Color(1, 1, 1, 1);
            foodImage.sprite = _food.foodImage;
            foodImage.SetNativeSize();
            if (foodImage.rectTransform.sizeDelta.x > foodImage.rectTransform.sizeDelta.y)
            {
                foodImage.rectTransform.sizeDelta = new Vector2(imageSize,
                   foodImage.rectTransform.sizeDelta.y
                   * (imageSize / foodImage.rectTransform.sizeDelta.x));
            }
            else
            {
                foodImage.rectTransform.sizeDelta = new Vector2(
                   foodImage.rectTransform.sizeDelta.x
                   * (imageSize / foodImage.rectTransform.sizeDelta.y),
                   imageSize);
            }
        }
    }
    //Ingredient _ingredient;
    /*public Ingredient ingredient 
    { 
        get 
        {  
            return _ingredient; 
        } 
        set 
        { 
            _ingredient = value;
            foodImage.sprite = _ingredient.ingredientImage;
            foodName.text = _ingredient.ingredientName;
            foodInfo.text = _ingredient.ingredientInfo;
        } 
    }*/
    Food _food;
    public Food food
    {
        get
        {
            return _food;
        }
        set
        {
            _food = value;
            foodImage.sprite = _food.foodImage;
            foodName.text = _food.foodName;
            foodInfo.text = _food.foodInfo;
        }
    }
}
