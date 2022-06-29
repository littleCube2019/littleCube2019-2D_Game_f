using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class init_world : MonoBehaviour
{
   Inventory equipment;
    generator Generator;
    int init_island_size  = 5; 
    IEnumerator Start()
    {


        GameObject GeneratorObj = GameObject.Find("generator");
        Generator = GeneratorObj.GetComponent<generator>() ;
        for(int i = -init_island_size ; i < init_island_size ; i++){
            Generator.PlaceBlock(new Vector2(i,0),0);
        }

        yield return new WaitForSeconds(0.1f);
        
        GameObject player = GameObject.Find("main_character");
        
        player.GetComponent<UI_Player>().inventory.AddItem(new Item(Item.ItemType.earthElement,11,ItemAssets.Instance.Info[(int)Item.ItemType.earthElement]));
        player.GetComponent<UI_Player>().inventory.AddItem(new Item(Item.ItemType.waterElement,3,ItemAssets.Instance.Info[(int)Item.ItemType.waterElement]));
        player.GetComponent<UI_Player>().inventory.AddItem(new Item(Item.ItemType.woodElement,5,ItemAssets.Instance.Info[(int)Item.ItemType.woodElement]));
        player.GetComponent<UI_Player>().inventory.AddItem(new Item(Item.ItemType.fireElement,5,ItemAssets.Instance.Info[(int)Item.ItemType.fireElement]));
        player.GetComponent<UI_Player>().inventory.AddItem(new Item(Item.ItemType.magicStick,1,ItemAssets.Instance.Info[(int)Item.ItemType.magicStick]));
         player.GetComponent<UI_Player>().inventory.AddItem(new Item(Item.ItemType.spear,1,ItemAssets.Instance.Info[(int)Item.ItemType.spear]));
        
        // add Event
        /*
        equipment = player.GetComponent<UI_Player>().equipmentInventory;
        equipment.OnItemListChanged += ChangePlayerValueByEquipement;
        */
    }
    private void ChangePlayerValueByEquipement(object sender, System.EventArgs e)
    {
      
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
