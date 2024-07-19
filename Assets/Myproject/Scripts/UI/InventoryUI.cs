using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI current;

    [SerializeField]
    InventorySlot inventorySlotPrefab;
    [SerializeField]
    GameObject inventoryBackground;
    List<InventorySlot> inventorySlots = new List<InventorySlot>();
    Dictionary<Ingredient, int> inventory = new Dictionary<Ingredient, int>();
    private void Awake()
    {
        current = this;
    }
    void Start()
    {
        inventory = Player.current.inventory;
        inventoryBackground.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            InventorySwitch();
        }
    }
    void InventorySwitch()
    {
        if(inventoryBackground.activeSelf == false)
        {
            inventoryBackground.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            inventoryBackground.SetActive(false);
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
            }
        }
    }
    void InventoryFreshSlot()
    {
        //자동 정렬 및 쓰이지 않는 슬롯 삭제
    }
}
