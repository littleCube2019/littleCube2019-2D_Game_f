using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player : MonoBehaviour
{
    [SerializeField] public UI_inventory permanentInventory_UI;
    [SerializeField] public UI_inventory inventory_UI;
    [SerializeField] public UI_inventory headEquipmentInventory_UI;
    [SerializeField] public UI_inventory bodyEquipmentInventory_UI;
    [SerializeField] public UI_inventory handEquipmentInventory_UI;
    [SerializeField] public UI_inventory legEquipmentInventory_UI;
    [SerializeField] public Transform Portrait;

    public Inventory permanentInventory;
    public Inventory inventory;
    public Inventory headEquipmentInventory;
    public Inventory bodyEquipmentInventory;
    public Inventory handEquipmentInventory;
    public Inventory legEquipmentInventory;

    private bool showInventory = false;
    private void Awake()
    {
        permanentInventory = new Inventory(10, permanentInventory_UI);
        permanentInventory_UI.SetInventory(permanentInventory);
        permanentInventory_UI.CreateSlot(1, 10);

        inventory = new Inventory(30, inventory_UI);
        inventory_UI.SetInventory(inventory);
        inventory_UI.CreateSlot(5, 6);

        bodyEquipmentInventory = new Inventory(1, bodyEquipmentInventory_UI);
        bodyEquipmentInventory_UI.SetInventory(bodyEquipmentInventory);
        bodyEquipmentInventory_UI.CreateSlot(1, 1);

        headEquipmentInventory = new Inventory(1, headEquipmentInventory_UI);
        headEquipmentInventory_UI.SetInventory(headEquipmentInventory);
        headEquipmentInventory_UI.CreateSlot(1, 1);

        handEquipmentInventory = new Inventory(1, handEquipmentInventory_UI);
        handEquipmentInventory.SetLimits(new List<Item.ItemType> { Item.ItemType.magicStick});
        handEquipmentInventory_UI.SetInventory(handEquipmentInventory);
        handEquipmentInventory_UI.CreateSlot(1, 1);

        legEquipmentInventory = new Inventory(1, legEquipmentInventory_UI);
        legEquipmentInventory_UI.SetInventory(legEquipmentInventory);
        legEquipmentInventory_UI.CreateSlot(1, 1);
    }

    private void Start()
    {
        permanentInventory_UI.gameObject.SetActive(true);
        inventory_UI.gameObject.SetActive(false);
        headEquipmentInventory_UI.gameObject.SetActive(false);
        bodyEquipmentInventory_UI.gameObject.SetActive(false);
        handEquipmentInventory_UI.gameObject.SetActive(false);
        legEquipmentInventory_UI.gameObject.SetActive(false);
        Portrait.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            showInventory = !showInventory;
            inventory_UI.gameObject.SetActive(showInventory);
            headEquipmentInventory_UI.gameObject.SetActive(showInventory);
            bodyEquipmentInventory_UI.gameObject.SetActive(showInventory);
            handEquipmentInventory_UI.gameObject.SetActive(showInventory);
            legEquipmentInventory_UI.gameObject.SetActive(showInventory);
            Portrait.gameObject.SetActive(showInventory);
        }
    }
}
