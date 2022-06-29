using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInventory : MonoBehaviour
{
    [SerializeField] private RectTransform UI;
    [SerializeField] public UI_inventory inventory_UI;
    [SerializeField] private int row;
    [SerializeField] private int col;

    public Inventory inventory;
    public List<Item.ItemType> limit = new List<Item.ItemType> {};
    private void Awake()
    {        

        inventory = new Inventory(row * col, inventory_UI);
        inventory.SetLimits(limit);
        inventory_UI.CreateSlot(row, col);
        inventory_UI.SetInventory(inventory);

    }
    private void Start()
    {
        //Focus(true, 0);
        //Focus(false);
    }
    public void SetPosition(RectTransform rectTransform)
    {
        //Camera.main.ScreenToWorldPoint(rectTransform.position);
        inventory_UI.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(Camera.main.ScreenToWorldPoint(rectTransform.position) + 1.2f * Vector3.up);
        
    }
    public void ShowInventory(bool show)
    {
        inventory_UI.gameObject.SetActive(show);
        if (show)
        {
            //Debug.Log("focus magic stick");
        }
    }
    /*
    public void Focus(bool focus, int index)
    {
        Debug.Log(name);
        GameObject player = GameObject.Find("main_character");
        //RectTransform rect = Camera.main.ScreenToWorldPoint(player.GetComponent<InventoryTemplate>().inventory_UI[1].transform.Find("slotContainer").GetChild(1).position);
        for (int i = 0; i < inventory_UI.Length; i++)
        {
            Vector3 t = Camera.main.ScreenToWorldPoint(player.GetComponent<InventoryTemplate>().inventory_UI[1].transform.Find("slotContainer").GetChild(index+1).GetComponent<RectTransform>().position);
            inventory_UI[i].GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(t + 2.7f * Vector3.down);
            //inventory_UI[i].GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.Find("UI_InventoryPosition").GetChild(i).position);
        }
        UI.gameObject.SetActive(focus);
    }
    */
}
