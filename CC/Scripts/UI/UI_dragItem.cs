using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_dragItem : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    
    [SerializeReference] Canvas canvas;
    public static UI_dragItem Instance { get; private set; }
    public CanvasGroup canvasGroup;
    public bool validPosition = true;
    public bool drop = false;

    private RectTransform rectTransform;
    private Vector2 currentPosition;
    private Item Item;
    private int index = 0;
    private void Awake()
    {
        Instance = this;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        currentPosition = rectTransform.anchoredPosition;
        validPosition = false;
        drop = true;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        //Debug.Log(validPosition);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (validPosition == false)
        {
            rectTransform.anchoredPosition = currentPosition;
        }
        if (drop)
        {
            transform.parent.parent.GetComponent<UI_inventory>().inventory.RemoveItemAt(index);
            GameObject.Find("DropItemManager").GetComponent<DropItemManager>().DropItem(canvas.transform.parent.transform.position, Item);
            Debug.Log("drop");
        }

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void SetItem(Item item)
    {
        this.Item = item;
        //Debug.Log(item.itemType);
    }

    public Item GetItem()
    {
        //Debug.Log("GetItem return " + Item.itemType.ToString());
        return Item;
    }
    public void SetIndex(int index)
    {
        this.index = index;
    }
    public int GetIndex()
    {
        return index;
    }

}
