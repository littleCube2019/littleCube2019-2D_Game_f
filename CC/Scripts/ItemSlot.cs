using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    Transform thisInventory;
    Transform itemInventory;
    int index;
    int itemIndex;
    private void Start()
    {
        thisInventory = transform.parent.parent;
        index = transform.GetSiblingIndex()-1;
    }
    public void OnDrop(PointerEventData eventData)
    {
        itemInventory = eventData.pointerDrag.transform.parent.parent;
        itemIndex = eventData.pointerDrag.GetComponent<UI_dragItem>().GetIndex();
        
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<UI_dragItem>().drop = false;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            
            Item item = eventData.pointerDrag.GetComponent<UI_dragItem>().GetItem();

            if(thisInventory==itemInventory && index == itemIndex)
            {
                eventData.pointerDrag.GetComponent<UI_dragItem>().validPosition = false;
                return;
            }

            if (thisInventory.GetComponent<UI_inventory>().inventory.AddItemAt(item, index)==null)
            {
                itemInventory.GetComponent<UI_inventory>().inventory.RemoveItemAt(itemIndex);
                eventData.pointerDrag.GetComponent<UI_dragItem>().validPosition = true;
            }
            else
            {
                eventData.pointerDrag.GetComponent<UI_dragItem>().validPosition = false;
            }
        }

    }
}
