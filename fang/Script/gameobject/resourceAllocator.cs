using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceAllocator : MonoBehaviour
{   
    /*
       0:bedRock,
       1:dirt,
       2:grass,
       3:treeLog,
       4:treeLeaf,
    */
    GameObject player;
    void Start(){
        player = GameObject.Find("main_character"); //for inventory
    }
    public void GetResource(int id){
        if(id == (int)generator.blockID.treeLog){
            
            player.GetComponent<UI_Player>().inventory.AddItem(new Item(Item.ItemType.wood,2 , ItemAssets.Instance.Info[(int)Item.ItemType.wood]));
        }
    }
   

}
