using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTemplate : MonoBehaviour
{
    [SerializeField] private RectTransform UI;
    [SerializeField] public UI_inventory[] inventory_UI;
    [SerializeField] private int[] row;
    [SerializeField] private int[] col;
    [SerializeField] private KeyCode trigger;
    [SerializeField] private bool needPlayerNearBy;

    public Inventory[] inventory;

    private bool playerIn;
    private bool activate;
    private void Awake()
    {
        int length = inventory_UI.Length;
        inventory = new Inventory[length];
        for(int i=0; i<inventory_UI.Length; i++)
        {
            inventory[i] = new Inventory(row[i] * col[i], inventory_UI[i]);
            inventory_UI[i].CreateSlot(row[i], col[i]);
            inventory_UI[i].SetInventory(inventory[i]);
        }

        activate = false;
        playerIn = false;
    }
    private void Update()
    {
        if (!needPlayerNearBy)
        {
            playerIn = true;
        }
        if (playerIn && Input.GetKeyDown(trigger))
        {
            activate = !activate;
        }
        for(int i=0; i<inventory_UI.Length; i++)
        {
            inventory_UI[i].GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(transform.Find("UI_InventoryPosition").GetChild(i).position);
        }
        UI.gameObject.SetActive(activate);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "main_character")
        {
            return;
        }
        playerIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "main_character")
        {
            return;
        }
        playerIn = false;
        activate = false;
    }
}
