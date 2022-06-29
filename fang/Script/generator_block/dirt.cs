using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dirt : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D boxCollider2D;
    bool isTransforming = false;
    private IEnumerator coroutine;
    int rain_counter = 0;
   
    [Header("transform")]
    [SerializeField] int changeTime = 10; 
    generator Generator;
    void Start() 
    {   
       GameObject GeneratorObj = GameObject.Find("generator");
       Generator = GeneratorObj.GetComponent<generator>() ;
       boxCollider2D = GetComponent<BoxCollider2D>(); 
       coroutine = TransformToGrass();
    }

    IEnumerator TransformToGrass(){
        int timer = 0;
        while(true){
            
            if(timer == changeTime){
                Generator.PlaceBlock(transform.position,(int)generator.blockID.grass);
                DestroyItself();
            }
            
            timer +=1;
            yield return new WaitForSeconds(1);
            
        }
    }
    // Update is called once per frame
    void Update()
    {

        if( canExist() ){
            /*
            if(!isTransforming){
                coroutine = TransformToGrass();
                StartCoroutine(coroutine);
                isTransforming = true;
            }*/
            if(rain_counter == 5){ 
                Generator.PlaceBlock(transform.position,(int)generator.blockID.grass);
                DestroyItself();
            }

        }
        else{
            DestroyItself();
        }
        
    }

    bool canExist(){
        return (GetFloorBlockDown() != null);
    }

    void DestroyItself(){
        StopCoroutine(coroutine);
        Destroy(gameObject);
    }
    
    Collider2D GetFloorBlockDown(){
        float halfBlockSize = 0.5f;
        Vector3 center = boxCollider2D.bounds.center;
        center += (boxCollider2D.bounds.extents.y + 0.01f) * Vector3.down;
        RaycastHit2D Hit = Physics2D.Raycast(center, Vector2.down , halfBlockSize  , (1<<6) );
        return Hit.collider;
    }

    private void OnTriggerEnter2D(Collider2D other) {
       Debug.Log(other.gameObject.layer );
       if (other.gameObject.layer == 7){
            
            if(other.gameObject.tag == "rain_drip" ){
                 rain_counter+=1;
            }
       }
    }
}
