using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class grass : MonoBehaviour
{
   

    BoxCollider2D boxCollider2D;

    
    
    [Header("tree generate")]
    [SerializeField] float treeChance = 0.1f;
    [SerializeField] int minTreeHeight = 1;
    [SerializeField] int maxTreeHeight = 4;
    generator Generator;
    bool isGrowing = false;

    IEnumerator GenerateTree(){
        while(true){
           if(treeChance >= Random.value){
              int treeHeight = Random.Range(minTreeHeight , maxTreeHeight);
           
              for(int i=1; i <=treeHeight ; i++ ){
                    Generator.PlaceBlock(transform.position + i*Vector3.up, (int)generator.blockID.treeLog);
              }
              Generator.PlaceBlock(transform.position + (treeHeight+1)*Vector3.up, (int)generator.blockID.treeLeaf);
              isGrowing=false;
              break;
                          
           }
           yield return new WaitForSeconds(1);
        }
    }
    void Start()
    {
        GameObject GeneratorObj = GameObject.Find("generator");
        Generator = GeneratorObj.GetComponent<generator>() ;
        boxCollider2D = GetComponent<BoxCollider2D>(); 
        
    }
    
     void Update(){
        if(isGrowing == false && GetAnyBlockAboveN(10) == null){
            isGrowing = true;
            StartCoroutine(GenerateTree());
        }
    }

    Collider2D GetAnyBlockAboveN(int n ){
        if (n<=0){
            Debug.Log("err, n should bigger than 0");
            return null;
        }
        float halfBlockSize = 0.5f;
        Vector3 center = boxCollider2D.bounds.center;
        center += (boxCollider2D.bounds.extents.y + 0.01f) * Vector3.up;
        RaycastHit2D Hit = Physics2D.Raycast(center, Vector2.up , halfBlockSize + (n-1)*2*halfBlockSize ); 
       
        return Hit.collider;
    }

    
}
