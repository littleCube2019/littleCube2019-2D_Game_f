
using UnityEngine;

[CreateAssetMenu(fileName = "item_obj", menuName = "item/item_obj")]
public class item_obj : ScriptableObject
{
    public Sprite sprite;
    public int maxAmount;
    

    public bool isDurability;

    public int durability;
    public int decreaseDurabilityPerHit;

}