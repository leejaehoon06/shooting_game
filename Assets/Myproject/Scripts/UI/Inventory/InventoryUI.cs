using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    static InventoryUI _current;
    public static InventoryUI current {  
        get 
        { 
            if (_current == null)
            {
                _current = FindObjectOfType<InventoryUI>(true);
            }
            return _current; 
        } 
    }

    [SerializeField]
    InventorySlot inventorySlotPrefab;
    [SerializeField]
    GameObject inventoryBackground;
    public List<InventorySlot> inventorySlots { get; private set; } = new List<InventorySlot>();
    void Start()
    {
        if (_current == null)
        {
            gameObject.SetActive(false);
        }
    }
    public void InventorySwitch()
    {
        if(gameObject.activeSelf == false && Time.timeScale > 0)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (gameObject.activeSelf == true)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void AddInventorySlot(Ingredient ingredient)
    {
        inventorySlots.Add(Instantiate(inventorySlotPrefab, inventoryBackground.transform));
        inventorySlots[inventorySlots.Count - 1].ingredientNum = 1;
        inventorySlots[inventorySlots.Count - 1].ingredient = ingredient;
    }
    public void AddIngredientNum(Ingredient ingredient)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].ingredient == ingredient)
            {
                inventorySlots[i].ingredientNum++;
                inventorySlots[i].UpdateSlot();
            }
        }
    }
    public void MinIngredient(Ingredient ingredient)
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].ingredient == ingredient)
            {
                inventorySlots[i].ingredientNum--;
                if (inventorySlots[i].ingredientNum <= 0)
                {
                    inventorySlots[i].ingredient = null;
                    InventoryFreshSlot();
                }
            }
        }
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].UpdateSlot();
        }
    }
    void InventoryFreshSlot()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].ingredient == null)
            {
                if (i == inventorySlots.Count - 1)
                {
                    Destroy(inventorySlots[i].gameObject);
                    inventorySlots.RemoveAt(i);
                }
                else if (i < inventorySlots.Count - 1)
                {
                    if (CheckNullSlot(i))
                    {
                        for (int j = i + 1; j < inventorySlots.Count; j++)
                        {
                            Destroy(inventorySlots[j].gameObject);
                            inventorySlots.RemoveAt(j);
                        }
                    }
                }
            }
        }
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].UpdateSlot();
        }
    }
    bool CheckNullSlot (int i)
    {
        for (int j = i + 1; j < inventorySlots.Count; j++)
        {
            if (inventorySlots[j].ingredient != null)
            {
                inventorySlots[i].ingredient = inventorySlots[j].ingredient;
                inventorySlots[i].ingredientNum = inventorySlots[j].ingredientNum;
                inventorySlots[i].UpdateSlot();
                inventorySlots[j].ingredient = null;
                inventorySlots[j].ingredientNum = 0;
                inventorySlots[j].UpdateSlot();
                return false;
            }
        }
        return true;
    }
}
