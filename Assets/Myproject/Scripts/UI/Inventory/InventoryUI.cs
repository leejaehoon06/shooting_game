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
    List<InventorySlot> inventorySlots = new List<InventorySlot>();
    void Start()
    {
        if (_current == null)
        {
            gameObject.SetActive(false);
        }
    }
    public void InventorySwitch()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
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
    void InventoryFreshSlot()
    {
        //자동 정렬 및 쓰이지 않는 슬롯 삭제
    }
}
