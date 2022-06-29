using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tool_functions : MonoBehaviour
{


    
    generator Generator;
    cloud Cloud ; 
    Inventory MagicStick;
    int ViewLimit = 5;
    GameObject player;
    Vector3 mainCharacterPos ; 
    weapon_manager Weapon_manager;
    // Start is called before the first frame update
   
    void Start()
    {   
        GameObject MagicStickObj = GameObject.Find("MagicStick");
        GameObject GeneratorObj = GameObject.Find("generator");
        GameObject CloudObj = GameObject.Find("cloud");
        Transform WeaponTrnas = transform.Find("weapon_manager");
        player = transform.gameObject;

        Weapon_manager = WeaponTrnas.gameObject.GetComponent<weapon_manager>();
        Generator = GeneratorObj.GetComponent<generator>() ;
        Cloud = CloudObj.GetComponent<cloud>();
        MagicStick = MagicStickObj.GetComponent<ItemInventory>().inventory; 
     
    }
    
  

    // Update is called once per frame
    public void usage(int id, Vector2 mousePos , int faceToRight=1 ,int mouseAtRight=1 , Item Focus_item=null) //TODO:  change id to Item (and get Item.Itemtype)
    {
        mainCharacterPos = transform.position ;
        
        mainCharacterPos.x = Mathf.RoundToInt(mainCharacterPos.x);
        mainCharacterPos.y = Mathf.RoundToInt(mainCharacterPos.y);
        if(id == 0){ //magic stick
            float r = 0.001f;
            
            //check what you clicked
            mousePos.x  = Mathf.RoundToInt(mousePos.x);
            mousePos.y  = Mathf.RoundToInt(mousePos.y);
            Collider2D collider = Physics2D.OverlapCircle(mousePos, r);
           

            
            
            Item firstItem = MagicStick.GetItemList()[0];
            
            if(firstItem == null){ //nothing in magic stick
                return ;
            }
            

            // check where you clicked is in the view range
            int Mdistance = (int)(Mathf.Abs(mousePos.x - mainCharacterPos.x) + Mathf.Abs(mousePos.y - mainCharacterPos.y));

            if(firstItem.itemType == Item.ItemType.woodElement ){
                if(collider == null && Mdistance <= ViewLimit && MatchTheRuleForEachElement(0,mousePos)) //clicked position is empty   &&  in the view range
                {   
                    
                    
                    MagicStick.RemoveItem(firstItem , 1);
                    Generator.PlaceBlock(mousePos, (int)generator.blockID.treeLeaf);
                }
            }

            if(firstItem.itemType == Item.ItemType.earthElement){
                if(collider == null && Mdistance <= ViewLimit && MatchTheRuleForEachElement(0,mousePos)) //clicked position is empty   &&  in the view range
                {   
                    
                    
                    MagicStick.RemoveItem(firstItem , 1);
                    Generator.PlaceBlock(mousePos, (int)generator.blockID.dirt);
                }
            }
  
            if(firstItem.itemType == Item.ItemType.waterElement){
                if(collider == null && Mdistance <= ViewLimit ) //clicked position is empty   &&  in the view range
                {   
                    MagicStick.RemoveItem(firstItem , 1);
                    Cloud.rainingNow();
                }
            }
            
           
        }
        else if(id == 1){ // spear
            
            Weapon_manager.usage((int)weapon_manager.weaponID.spear ,mousePos , faceToRight , mouseAtRight );
            //Focus_item.decreaseDurability();
        }
    }

    public bool MatchTheRuleForEachElement(int id , Vector3 center){
        if(id == 0){ //earth 
            float halfBlockSize = 0.5f;

            center += (halfBlockSize + 0.01f) * Vector3.down;
            RaycastHit2D Hit = Physics2D.Raycast(center, Vector2.down , halfBlockSize  , (1<<6) );
            if(Hit.collider != null)
            {
               return true;
            }
            return false;

        }
        return true;
    }
}
