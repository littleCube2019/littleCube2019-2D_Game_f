using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [Header("control flags")]
    [SerializeField] int fuel = 5; 
    [SerializeField] float JumpForce = 200f;
    [SerializeField] float MaxJumpingHeight =1.5f;
    [SerializeField] bool backpack = false;
    [SerializeField] bool onGround = true;

    /*
    0 : magic tools
    */
    public int focusItemNo ; 
    Rigidbody2D rb2D;
    BoxCollider2D boxCollider2D;
    private IEnumerator FuleConsumeAdress;
    generator Generator;
    
    int characterFaceRight = 1;
  
    float gravityScale = 1f;
    float main_character_mass = 1f;
    void Start() 
    {   
        
       GameObject GeneratorObj = GameObject.Find("generator");
       Generator = GeneratorObj.GetComponent<generator>() ;
       
       focusItemNo = 0;

       
       JumpForce = Mathf.Sqrt((MaxJumpingHeight * (2f*gravityScale*9.8f) * main_character_mass));
       boxCollider2D = GetComponent<BoxCollider2D>(); 
       rb2D = GetComponent<Rigidbody2D>();
        //Debug.Log("aaaa");
    }





    IEnumerator fuelConsume(){
        while(true){
            fuel -= 1;
            yield return new WaitForSeconds(1);
        }
    }
    private bool isGround(){
        Vector2 Center = boxCollider2D.bounds.center;
        Center.y -= boxCollider2D.bounds.extents.y;
        Collider2D collider = Physics2D.OverlapBox(Center, new Vector2(0.9f*2*boxCollider2D.bounds.extents.x , 0.1f) ,0 , (1<<6) );

        if(collider!= null   ){
          
            return true;    
        }
        else{
            return false;
        }
        
    } 
    /*
    void OnDrawGizmos()
    {
        Vector2 Center = boxCollider2D.bounds.center;
        Center.y -= boxCollider2D.bounds.extents.y;
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawCube(Center, new Vector2(2*boxCollider2D.bounds.extents.x , 0.1f));
    }*/

    KeyCode [] digitsKey = {KeyCode.Alpha0,KeyCode.Alpha1,KeyCode.Alpha2,KeyCode.Alpha3,KeyCode.Alpha4,KeyCode.Alpha5,KeyCode.Alpha6,KeyCode.Alpha7,KeyCode.Alpha8,KeyCode.Alpha9};
    // Update is called once per frame
    void Update()
    {   
        //change focus item 
        for(int i = 0 ; i < 10 ; i++){
            if( Input.GetKeyDown(digitsKey[i])) {
                focusItemNo = (i+9)%10;
            } 
        }
        transform.GetComponent<UI_Player>().permanentInventory_UI.SetFocus(focusItemNo);


    
        
        
        onGround = isGround();
        

        if( backpack && (fuel == 0) ){
            StopCoroutine(FuleConsumeAdress);
            rb2D.gravityScale = 1;
            backpack = !backpack; 
        }
        
      

        if(Input.GetKeyDown(KeyCode.Q)){


            backpack = !backpack; 
            if(backpack && fuel > 0){
                rb2D.gravityScale = 0;
                rb2D.velocity = new Vector2(0.0f, 0.0f);
                FuleConsumeAdress = fuelConsume();
                StartCoroutine(FuleConsumeAdress);
            }
            else{
                StopCoroutine(FuleConsumeAdress);
                rb2D.gravityScale = 1;
            }
        }   
        if(Input.GetKey(KeyCode.A)){
            characterFaceRight = 1;
            
            float extra_height = 0.02f;
            RaycastHit2D Hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.left , boxCollider2D.bounds.extents.x + extra_height , (1<<6) );

            if(Hit.collider == null   ){
                transform.Translate(-0.01f,0,0);
            }
            
        } 
        if(Input.GetKey(KeyCode.D)){     
            characterFaceRight = -1;
            
            float extra_height = 0.02f;
            RaycastHit2D Hit = Physics2D.Raycast(boxCollider2D.bounds.center, Vector2.right , boxCollider2D.bounds.extents.x + extra_height , (1<<6) );

            if(Hit.collider == null ){
                transform.Translate(0.01f,0,0);
            }
            
        }
      
        if(Input.GetKey(KeyCode.W)){
            if(onGround){
                rb2D.velocity = new Vector2(0.0f, 0.0f);
                rb2D.AddForce(new Vector3(0,JumpForce,0) , ForceMode2D.Impulse);
                
            }
            if(backpack){
                transform.Translate(0,0.01f,0);
            }
        }
        if(Input.GetKey(KeyCode.S)){
            if(!onGround){
                transform.Translate(0,-0.01f,0);
            }
        }
        
        /*if(Input.GetKeyDown(KeyCode.Space)){ //hit myself
            GameObject.Find("Enemy").GetComponent<enemy_status>().takeDamage(10);
        }*/



        
        if (Input.GetMouseButtonDown(0)){
            Vector2 mousePos ;
            mousePos.x  = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            mousePos.y  = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            int mouseAtRelativeRight = 1;
            if(mousePos.x > transform.position.x){
                mouseAtRelativeRight = 1;
                transform.localScale = new Vector3(mouseAtRelativeRight,1,1) * 0.9f;
            }
            else{
                mouseAtRelativeRight = -1;
                transform.localScale = new Vector3(mouseAtRelativeRight,1,1) * 0.9f;
            }
            //get item at that position
       
            
            Inventory Focus_inv= gameObject.GetComponent<UI_Player>().permanentInventory;
            Focus_inv.UseItem(focusItemNo);
            //get item player is focusing on 
            Item Focus_item =  Focus_inv.GetItemList()[focusItemNo];
            
            // no focus item !!!!;
            if(Focus_item == null){
                return ;
            }
            else if( Focus_item.itemType == Item.ItemType.magicStick  ){ //magic stick
                gameObject.GetComponent<tool_functions>().usage(0 ,mousePos ,characterFaceRight , mouseAtRelativeRight);
            } 
            else if( Focus_item.itemType == Item.ItemType.spear){   // spear 
                gameObject.GetComponent<tool_functions>().usage(1 ,mousePos ,characterFaceRight ,mouseAtRelativeRight ,Focus_item);
            }

            
           

        }
        if( Input.GetMouseButtonDown(1)){
           float r = 0.001f;
            
            Vector2 mousePos ;
            mousePos.x  = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).x);
            mousePos.y  = Mathf.RoundToInt(Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            Collider2D collider = Physics2D.OverlapCircle(mousePos, r);
           
            if(collider != null)
            {
                 
                Generator.RemoveBlock(collider.gameObject);
            }


        }

        
    }
    
   



}
