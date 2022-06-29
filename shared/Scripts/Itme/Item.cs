using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemType itemType;
    public int amount;
    public int maxAmount = 10;
    public bool hasInventory;
    public int durability;
    public bool isDurability;
    public int decreaseDurabilityPerHit;
    public enum ItemType
    {
    
        wood,
        woodElement,
        magicStick,
        earthElement,
        waterElement,
        fireElement,
        spear
    }
    
   
    public Item(ItemType type ,int amount,item_obj Info)
    {
        this.itemType = type;
        this.amount = amount;
        this.hasInventory = false;
        this.maxAmount = Info.maxAmount;
        this.durability = Info.durability;
        this.isDurability = Info.isDurability;
        this.decreaseDurabilityPerHit = Info.decreaseDurabilityPerHit;
    }

    public void decreaseDurability(){
        durability -= decreaseDurabilityPerHit;
    }

    public Sprite GetSprite()
    {            
        return ItemAssets.Instance.Info[(int)itemType].sprite;
    }
}
