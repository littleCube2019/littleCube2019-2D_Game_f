using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    [SerializeField] GameObject dropTemplate;

     private List<Item.ItemType> enemyDrop = new List<Item.ItemType>
    {
        Item.ItemType.magicStick,
    };
    public void EnemyDropItem(Vector3 position , int id)
    {
        
        if (enemyDrop[id] == null)
        {
            return;
        }
        Item newItem = new Item(enemyDrop[id], 1 , ItemAssets.Instance.Info[(int)enemyDrop[id]] );
        GameObject drop = Instantiate(dropTemplate);
           
        drop.GetComponent<DroppedItem>().SetItem(newItem);
    
        drop.GetComponent<Transform>().position = position;
        drop.gameObject.SetActive(true);
    }
    public void DropItem(Vector3 position, Item item)
    {
        GameObject drop = Instantiate(dropTemplate);
        drop.GetComponent<DroppedItem>().SetItem(item);
        drop.GetComponent<Transform>().position = position + 2*((float)Random.Range(0,2)-0.5f) * Vector3.left;
        drop.gameObject.SetActive(true);
    }
}
