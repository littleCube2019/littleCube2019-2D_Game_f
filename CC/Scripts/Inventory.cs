using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public event EventHandler OnItemListChanged;

    //private List<Item> itemList;
    private Item[] itemList;
    private int capacity;
    private UI_inventory inventoryUI;
    private List<Item.ItemType> limits;
    private GameObject dropManager;
    public Inventory(int capacity, UI_inventory UI)
    {
        dropManager = GameObject.Find("DropItemManager");
        //itemList = new List<Item>();
        this.capacity = capacity;
        itemList = new Item[capacity];
        limits = new List<Item.ItemType> { };
        InitItemList();
    }
    public Item[] GetItemList()
    {
        return itemList ;
    }
    public void SetLimits(List<Item.ItemType> limits)
    {
        this.limits = limits;
    }
    public Item AddItem(Item item)
    {
        //Debug.Log("add item: " + item.itemType.ToString());

        //Debug.Log(item.amount);
        if (limits.Count > 0 && !limits.Contains(item.itemType))
        {
            return item;
        }
        for(int i=0; i<capacity; i++)
        {
            if (itemList[i] != null)
            {
                if (itemList[i].itemType == item.itemType && itemList[i].amount<itemList[i].maxAmount)
                {
                    if (item.amount + itemList[i].amount > itemList[i].maxAmount)
                    {
                        int part1 = itemList[i].maxAmount - itemList[i].amount;
                        int part2 = item.amount - part1;
                        itemList[i].amount = itemList[i].maxAmount;
                        OnItemListChanged?.Invoke(this, EventArgs.Empty);
                        return AddItem(new Item(item.itemType, part2 ,ItemAssets.Instance.Info[(int)item.itemType]));
                    }
                    else
                    {
                        itemList[i].amount += item.amount;
                        OnItemListChanged?.Invoke(this, EventArgs.Empty);
                        return null;
                    }
                }
            }
        }
        //Debug.Log("add item " + item.itemType.ToString());
        for(int i=0; i<capacity; i++)
        {
            if (itemList[i] == null)
            {
                if (item.amount <= item.maxAmount)
                {
                    itemList[i] = item;
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
                    return null;
                }
                else
                {
                    int part1 = item.maxAmount;
                    int part2 = item.amount - part1;
                    itemList[i] = new Item(item.itemType, item.maxAmount,ItemAssets.Instance.Info[(int)item.itemType]);
                    OnItemListChanged?.Invoke(this, EventArgs.Empty);
                    return AddItem(new Item(item.itemType, part2,ItemAssets.Instance.Info[(int)item.itemType]));
                }
            }
        }
        //inventoryUI.RefreshInventory();
        //OnItemListChanged?.Invoke(this, EventArgs.Empty);
        return item;
    }
    public void UseItem(int index)
    {
        
    
        if (itemList[index] == null || !itemList[index].isDurability)
        {
            return;
        }
        itemList[index].durability -= itemList[index].decreaseDurabilityPerHit;
        if (itemList[index].durability <= 0)
        {
            RemoveItemAt(index);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public Item AddItemAt(Item item, int index)
    {
        if (limits.Count>0 && !limits.Contains(item.itemType))
        {
            return item;
        }
        //Debug.Log(index);
        if(itemList[index]!=null && itemList[index].itemType == item.itemType)
        {
            if (item.amount + itemList[index].amount > itemList[index].maxAmount)
            {
                int part1 = itemList[index].maxAmount - itemList[index].amount;
                int part2 = item.amount - part1;
                itemList[index].amount = itemList[index].maxAmount;
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return AddItem(new Item(item.itemType, part2,ItemAssets.Instance.Info[(int)item.itemType]));
            }
            else
            {
                itemList[index].amount += item.amount;
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return null;
            }
        }
        if (itemList[index] == null)
        {
            if (item.amount <= item.maxAmount)
            {
                itemList[index] = item;
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return null;
            }
            else
            {
                int part1 = item.maxAmount;
                int part2 = item.amount - part1;
                itemList[index] = new Item(item.itemType, item.maxAmount,ItemAssets.Instance.Info[(int)item.itemType]);
                OnItemListChanged?.Invoke(this, EventArgs.Empty);
                return AddItem(new Item(item.itemType, part2,ItemAssets.Instance.Info[(int)item.itemType]));
            }
        }
        else
        {
            Debug.Log("already has item");
            return item;
        }

    }
    public void RemoveItem(Item item)
    {
        for (int i = 0; i < capacity; i++)
        {
            if (itemList[i] != null && itemList[i].itemType == item.itemType)
            {
                itemList[i] = null;
                break;
            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void RemoveItem(Item item, int amount)
    {
        for(int i=0; i<capacity; i++)
        {
            if (itemList[i] != null && itemList[i].itemType == item.itemType)
            {
                itemList[i].amount -= amount;
                if (itemList[i].amount <= 0)
                {
                    itemList[i] = null;
                }
                break;
            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RemoveItemAt(int index)
    {
        itemList[index] = null;
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    //need fixed
    public void RemoveItemAt(int index, int amount)
    {
        if (itemList[index] != null)
        {
            itemList[index].amount -= amount;
            if (itemList[index].amount <= 0)
            {
                itemList[index] = null;
            }
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void CombineItem(int index1, int index2)
    {
        Debug.Log("combine");
        if (itemList[index1].itemType != itemList[index2].itemType)
        {
            return;
        }
        if(itemList[index1].amount+ itemList[index2].amount< itemList[index1].maxAmount)
        {
            itemList[index1].amount += itemList[index2].amount;
            RemoveItemAt(index2);
        }
        else
        {
            int left = itemList[index1].amount + itemList[index2].amount - itemList[index1].maxAmount;
            itemList[index1].amount = itemList[index2].maxAmount;
            itemList[index2].amount = left;
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }
    public void InitItemList()
    {
        for (int i = 0; i < capacity; i++)
        {
            itemList[i] = null;
        }
        //Debug.Log(itemList.Length);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public int GetCapacity()
    {
        return capacity;
    }
}
