using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class itemSlotForItem : MonoBehaviour, IDropHandler
{
    Transform thisInventory;
    Transform itemInventory;
    int index;
    int targetIndex;
    private Item item;
    private void Start()
    {
        thisInventory = transform.parent.parent;
        index = transform.GetComponent<UI_dragItem>().GetIndex();
        item = transform.GetComponent<UI_dragItem>().GetItem();
    }
    public void OnDrop(PointerEventData eventData)
    {
        eventData.pointerDrag.GetComponent<UI_dragItem>().drop = false;
        itemInventory = eventData.pointerDrag.transform.parent.parent;
        Item targetItem = eventData.pointerDrag.GetComponent<UI_dragItem>().GetItem();
        targetIndex = eventData.pointerDrag.GetComponent<UI_dragItem>().GetIndex();
        if(item.itemType == eventData.pointerDrag.GetComponent<UI_dragItem>().GetItem().itemType)
        {
            if (itemInventory == thisInventory)
            {
                itemInventory.GetComponent<UI_inventory>().inventory.CombineItem(index, targetIndex);
            }
            else
            {
                itemInventory.GetComponent<UI_inventory>().inventory.RemoveItemAt(targetIndex);
                Item leftItem =  thisInventory.GetComponent<UI_inventory>().inventory.AddItemAt(targetItem, index);
                if (leftItem != null)
                {
                    itemInventory.GetComponent<UI_inventory>().inventory.AddItemAt(leftItem, targetIndex);
                }
            }
            
        }
        else
        {
            thisInventory.GetComponent<UI_inventory>().inventory.RemoveItemAt(index);
            itemInventory.GetComponent<UI_inventory>().inventory.RemoveItemAt(targetIndex);
            thisInventory.GetComponent<UI_inventory>().inventory.AddItemAt(targetItem, index);
            itemInventory.GetComponent<UI_inventory>().inventory.AddItemAt(item, targetIndex);
        }
    }
}
