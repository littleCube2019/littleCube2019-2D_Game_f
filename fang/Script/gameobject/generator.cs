using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generator : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] List<GameObject> blockList;
    resourceAllocator resource_allocater;
    public enum blockID
    {
        bedRock,
        dirt,
        grass,
        treeLog,
        treeLeaf,
    }
        
    void Start(){
        GameObject ra = GameObject.Find("resourceAllocator");
        resource_allocater = ra.GetComponent<resourceAllocator>();

    }
    public void PlaceBlock(Vector2 pos , int id){
        
        GameObject newBlock = Instantiate(blockList[id], new Vector3(0, 0, 0), Quaternion.identity);
        newBlock.transform.position =  new Vector3(pos.x ,pos.y ,0) ;
    }

    public void RemoveBlock(GameObject obj){
        
        if(obj.GetComponent<readBlockInfo>()==null){
            return ; // not a block
        }
        
        obj.GetComponent<readBlockInfo>().hp-=1;
  
        if(obj.GetComponent<readBlockInfo>().hp == 0){
            
            resource_allocater.GetResource(obj.GetComponent<readBlockInfo>().id);
            
            Destroy(obj);
        }
    }

}
