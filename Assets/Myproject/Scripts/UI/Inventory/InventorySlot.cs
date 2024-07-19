using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Image image;

    [SerializeField]
    Image ingredientImage;
    [SerializeField]
    Text ingredientNumText;
    Ingredient _ingredient;
    public int ingredientNum { get; set; } = 1;
    public Ingredient ingredient {  
        get { return _ingredient; } 
        set 
        { 
            _ingredient = value; 
            UpdateSlot();
        } 
    }
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    public void UpdateSlot()
    {
        if(_ingredient == null)
        {
            ingredientImage.color = new Color(1, 1, 1, 0);
            ingredientNumText.text = "";
        }
        else
        {
            ingredientImage.color = new Color(1, 1, 1, 1);
            ingredientImage.sprite = _ingredient.ingredientImage;
            ingredientImage.SetNativeSize();
            ingredientNumText.text = ingredientNum.ToString();
        }
    }
}
