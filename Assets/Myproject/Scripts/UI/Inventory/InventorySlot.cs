using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IPointerClickHandler
{
    Image image;
    BoxCollider2D coll;
    [SerializeField]
    bool LevelUpSlot = false;

    [SerializeField]
    Image ingredientImage;
    [SerializeField]
    Text ingredientNumText;
    Ingredient _ingredient;
    public int ingredientNum { get; set; } = 1;
    [SerializeField]
    float imageSize;
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
        coll = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        coll.size = new Vector2(imageSize, imageSize);
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
            if(ingredientImage.rectTransform.sizeDelta.x > ingredientImage.rectTransform.sizeDelta.y)
            {
                ingredientImage.rectTransform.sizeDelta = new Vector2(imageSize,
                   ingredientImage.rectTransform.sizeDelta.y 
                   * (imageSize / ingredientImage.rectTransform.sizeDelta.x));
            }
            else
            {
                ingredientImage.rectTransform.sizeDelta = new Vector2(
                   ingredientImage.rectTransform.sizeDelta.x
                   * (imageSize / ingredientImage.rectTransform.sizeDelta.y), 
                   imageSize);
            }
            ingredientNumText.text = ingredientNum.ToString();
        }
    }
    public void ResetImageColor()
    {
        ingredientImage.color = new Color(1, 1, 1, 1);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(LevelUpSlot == true && LevelUpInventoryUI.current.clickIngredient == null && ingredientNum > 0)
        {
            LevelUpInventoryUI.current.clickIngredient = ingredient;
            LevelUpInventoryUI.current.clickIngredientNum = ingredientNum;
            ingredientImage.color = new Color(1, 1, 1, 0.5f);
        }
        else if (LevelUpSlot == true && LevelUpInventoryUI.current.clickIngredient != null)
        {
            LevelUpInventoryUI.current.SlotReset();
            LevelUpInventoryUI.current.clickIngredient = ingredient;
            LevelUpInventoryUI.current.clickIngredientNum = ingredientNum;
            ingredientImage.color = new Color(1, 1, 1, 0.5f);
        }
    }
}
