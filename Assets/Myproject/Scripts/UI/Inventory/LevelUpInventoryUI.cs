using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpInventoryUI : MonoBehaviour
{
    static LevelUpInventoryUI _current;
    public static LevelUpInventoryUI current
    {
        get
        {
            if (_current == null)
            {
                _current = FindObjectOfType<LevelUpInventoryUI>(true);
            }
            return _current;
        }
    }

    [SerializeField]
    InventorySlot inventorySlotPrefab;
    [SerializeField]
    GameObject inventorySlotBackground;
    List<InventorySlot> inventorySlots = new List<InventorySlot>();
    [SerializeField]
    GameObject foodImportObj;

    public Ingredient clickIngredient { get; set; }
    public int clickIngredientNum { get; set; }
    void Start()
    {
        if (_current == null)
        {
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        Time.timeScale = 0;
        if(Input.GetKeyDown(KeyCode.Escape) && foodImportObj.activeSelf == false)
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void LevelUp()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
        InventoryFreshSlot();
    }
    private void OnDisable()
    {
        for(int i=0;i<inventorySlots.Count; i++)
        {
            Destroy(inventorySlots[i].gameObject);
        }
        inventorySlots = new List<InventorySlot>();
    }
    public void InventoryFreshSlot()
    {
        for (int i = 0; i < InventoryUI.current.inventorySlots.Count; i++)
        {
            if (i <= inventorySlots.Count - 1)
            {
                inventorySlots[i].ingredient = InventoryUI.current.inventorySlots[i].ingredient;
                inventorySlots[i].ingredientNum = InventoryUI.current.inventorySlots[i].ingredientNum;
                inventorySlots[i].UpdateSlot();
            }
            else
            {
                inventorySlots.Add(Instantiate(inventorySlotPrefab, inventorySlotBackground.transform));
                inventorySlots[inventorySlots.Count - 1].ingredient = InventoryUI.current.inventorySlots[i].ingredient;
                inventorySlots[inventorySlots.Count - 1].ingredientNum = InventoryUI.current.inventorySlots[i].ingredientNum;
                inventorySlots[inventorySlots.Count - 1].UpdateSlot();
            }
        }
        for (int i = InventoryUI.current.inventorySlots.Count; i < inventorySlots.Count; i++)
        {
            Destroy(inventorySlots[i].gameObject);
            inventorySlots.RemoveAt(i);
        }
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].ingredientNum == 0)
            {
                Destroy(inventorySlots[i].gameObject);
                inventorySlots.RemoveAt(i);
                i--;
            }
        }
    }
    public void SlotReset()
    {
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].ingredient == clickIngredient)
            {
                inventorySlots[i].ResetImageColor();
                clickIngredient = null;
                inventorySlots[i].ingredientNum = clickIngredientNum;
                inventorySlots[i].UpdateSlot();
                clickIngredientNum = 0;
                break;
            }
        }
    }
}
