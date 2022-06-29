using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class status : MonoBehaviour
{
    // Start is called before the first frame update
    int systemMaxHp = 41;
    [SerializeField] int maxHp = 40 ; 
    [SerializeField] int Hp = 40 ; 
    [SerializeField] RectTransform HpBar ; 
    int basic_index = 2;

    void Awake(){
        
        for(int i = 0 ; i < systemMaxHp ; i++ ){
   
            HpBar.GetChild(basic_index + i).gameObject.SetActive(false) ;
            HpBar.GetChild(basic_index + i).GetChild(0).gameObject.SetActive(false) ;
            
        }
        setHp(5);
        setMaxHp(10);
       
    }
    public void setMaxHp(int v){
        
        
        RectTransform right = HpBar.GetChild(0).GetComponent<RectTransform>() ;
        
        //show bar container
        for(int i = 0 ; i < maxHp ; i++ ){
        
            HpBar.GetChild(basic_index + i).gameObject.SetActive(false) ;
            HpBar.GetChild(basic_index + i).GetChild(0).gameObject.SetActive(false) ;
            
        }
        for(int i = 0 ; i < v ; i ++){
            HpBar.GetChild(basic_index + i).gameObject.SetActive(true) ;
            if(i < Hp){
                HpBar.GetChild(basic_index + i).GetChild(0).gameObject.SetActive(true) ;
            }
        }
        maxHp = v;
        //set right battery cap
        right.anchoredPosition = new Vector2( -485f + 20f*maxHp  , -275.5f);
    }

    public void setHp( int v ){
        v = Mathf.Min(v , maxHp);
        for(int i = 0 ; i < v ; i++){
            HpBar.GetChild(basic_index + i).GetChild(0).gameObject.SetActive(true) ;
        }
        Hp = v;
    }

    public void takeDamage(int v){
        v = Mathf.Min(Hp,v);
        
        for(int i = Hp-v ; i < Hp  ; i++){
            HpBar.GetChild(basic_index + i).GetChild(0).gameObject.SetActive(false) ;
        }

        Hp -= v;
               
    }
    
    public void takeHeal(int v){
        
        
        int tmp = Hp + v;
        tmp = Mathf.Min(tmp , maxHp);

        for(int i = Hp ; i < tmp  ; i++){
            HpBar.GetChild(basic_index + i).GetChild(0).gameObject.SetActive(true) ;
        }
        Hp = tmp;
        
    }


   
   
}
