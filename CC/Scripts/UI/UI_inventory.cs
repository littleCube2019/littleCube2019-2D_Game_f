using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_inventory : MonoBehaviour
{
    public Inventory inventory;

    float slotSize = 50;

    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemSlotTemplate;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private Transform slotTemplate;


    //private Transform itemSlotContainer;
    //private Transform itemSlotTemplate;
    //private Transform slotContainer;
    //private Transform slotTemplate;
    private float row = 0;
    private float col = 0;
    private float initPos_x = 0;
    private float initPos_y = 0;
    private float capacity;
    private int focusIndex = -1;
    private void Awake()
    {
        //itemSlotContainer = transform.Find("itemSlotContainer");
        //itemSlotTemplate = itemSlotContainer.Find("itemSlotTemplate");
        //slotContainer = transform.Find("slotContainer");
        //slotTemplate = slotContainer.Find("slotTemplate");
    }


    public void CreateSlot(float row, float col)
    {
        this.row = row;
        this.col = col;
        if (slotTemplate == null)
        {
            Debug.Log(name + "slot template not found");
            return;
        }
        initPos_x = -((col - 1) / 2) * slotSize;
        initPos_y = ((row - 1) / 2) * slotSize;
        for (int i=0; i<row; i++)
        {
            for(int j=0; j<col; j++)
            {
                RectTransform slotRectTransform = Instantiate(slotTemplate, slotContainer).GetComponent<RectTransform>();
                slotRectTransform.anchoredPosition = new Vector2(initPos_x + j * slotSize, initPos_y - i * slotSize);
                slotRectTransform.gameObject.SetActive(true);
            }
        }
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        this.capacity = inventory.GetCapacity();
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefreshInventory();
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefreshInventory();
    }
    public void RefreshInventory()
    {
        
        if (itemSlotContainer != null)
        {
            
            foreach (Transform child in itemSlotContainer)
            {
                if (child == itemSlotTemplate) continue;
                Destroy(child.gameObject);
            }
            
            Item[] itemList = inventory.GetItemList();
            for(int i=0; i<inventory.GetCapacity(); i++)
            {
                if (itemList[i] != null)  // i-th itemSlot is not empty
                {

                   
                    RectTransform itemSlotRectTransform = Instantiate(itemSlotTemplate, itemSlotContainer).GetComponent<RectTransform>();
                    itemSlotRectTransform.gameObject.SetActive(true);
                    
                    if(itemList[i].isDurability == true){
                        
                        itemSlotRectTransform.GetChild(2).gameObject.SetActive(true);
                        itemSlotRectTransform.GetComponent<enemy_status>().setHp(itemList[i].durability) ;
                        //a.Start();
                        
                    }
                
                    
                    
                    //UI_dragItem.Instance.SetItem(item);
                    itemSlotRectTransform.gameObject.GetComponent<UI_dragItem>().SetItem(itemList[i]);
                    itemSlotRectTransform.gameObject.GetComponent<UI_dragItem>().SetIndex(i);
                    itemSlotRectTransform.position = slotContainer.GetChild(i + 1).GetComponent<RectTransform>().position;
                    Image image = itemSlotRectTransform.Find("Image").GetComponent<Image>();
                    image.sprite = itemList[i].GetSprite();
                    Text text = itemSlotRectTransform.Find("Text").GetComponent<Text>();
                    text.text = itemList[i].amount.ToString();
                }
            }
        }
        else
        {
            Debug.Log("something wrong");
        }
    }

    public void SetFocus(int index)
    { 
        for (int i=0; i<slotContainer.childCount-1; i++)
        {
            if (i == index)
            {
                slotContainer.GetChild(i + 1).Find("Focus").gameObject.SetActive(true);
                if (inventory.GetItemList()[i] != null)
                {
                    if (inventory.GetItemList()[i].itemType == Item.ItemType.magicStick)
                    {
                        GameObject.Find("MagicStick").GetComponent<ItemInventory>().SetPosition(slotContainer.GetChild(i + 1).GetComponent<RectTransform>());
                        GameObject.Find("MagicStick").GetComponent<ItemInventory>().ShowInventory(true);
                    }
                    else
                    {
                        GameObject.Find("MagicStick").GetComponent<ItemInventory>().ShowInventory(false);
                    }
                }
                else
                {
                    GameObject.Find("MagicStick").GetComponent<ItemInventory>().ShowInventory(false);
                }
            }
            else
            {
                slotContainer.GetChild(i + 1).Find("Focus").gameObject.SetActive(false);
            }
        }
        /*
        if (inventory.GetItemList()[index].itemType==Item.ItemType.magicStick)
        {
            slotContainer.GetChild(index + 1).Find("Focus").gameObject.SetActive(true);
        }
        */
    }
}
