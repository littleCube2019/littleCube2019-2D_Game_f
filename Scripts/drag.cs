using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeReference] Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 currentPosition;
    public bool validPosition = true;
    private void Awake()
    {
        rectTransform = GetComponent<CircleTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        currentPosition = rectTransform.anchoredPosition;
        validPosition = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        Debug.Log(validPosition);
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (validPosition == false)
        {
            rectTransform.anchoredPosition = currentPosition;
        }

    }

}
