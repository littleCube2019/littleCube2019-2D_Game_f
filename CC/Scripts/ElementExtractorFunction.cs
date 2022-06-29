using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementExtractorFunction : MonoBehaviour
{
    private Inventory input;
    private Inventory output;
    private IEnumerator coroutine;

    private void Start()
    {
        input = transform.GetComponent<InventoryTemplate>().inventory[0];
        output = transform.GetComponent<InventoryTemplate>().inventory[1];
    }

    private void Update()
    {
        if (input.GetItemList()[0]!=null)
        {
            if(input.GetItemList()[0].itemType == Item.ItemType.wood)
            {
                if (coroutine == null)
                {
                    coroutine = Extract(2);
                    StartCoroutine(coroutine);
                }
            }
        }
        else
        {
            StopAllCoroutines();
            coroutine = null;
        }
    }
    private IEnumerator Extract(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            input.RemoveItemAt(0, 1);
            output.AddItem(new Item(Item.ItemType.woodElement,1,ItemAssets.Instance.Info[(int)Item.ItemType.woodElement]));
            coroutine = null;
            break;
        }
    }
}
