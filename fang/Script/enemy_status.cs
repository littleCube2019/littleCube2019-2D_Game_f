using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemy_status : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    [SerializeField] int id = 0;
    [SerializeField] int maxHp = 100 ; 
    [SerializeField] int Hp = 100 ; 
    [SerializeField] Gradient hpGradient ; 
    public GameObject hpBarObj;
    [SerializeField] Slider hpBar ; 
    
    [SerializeField] Image hpFill ; 

    public void Awake()
    {
        hpBar.maxValue = maxHp;
        hpBar.value = Hp;
    }





    public void setMaxHp(int v){
        maxHp = v;
        Hp = v;
        hpBar.value = v;
        hpFill.color = hpGradient.Evaluate(1f);
    }

    public void takeDamage(int v){
        v = Mathf.Min(Hp,v);
        Hp -= v;
        if (Hp <= 0)
        {
            Killed();
        }
        Debug.Log(hpBar);
        hpBar.value = Hp;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }
    
    public void setHp(int v){
        
        v = Mathf.Max(v,0);
        v = Mathf.Min(v,maxHp);
        
        hpBar.value = v;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }

    public void takeHeal(int v){
        Hp += v;
        Hp = Mathf.Min(Hp , maxHp);
        hpBar.value = Hp;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
       
        if(other.gameObject.layer == 8){ // isWeapon
            readWeaponInfo info =  other.gameObject.GetComponent<readWeaponInfo>();
            if(info == null) return ;
            takeDamage(info.attack);
        }
    }

    private void Killed()
    {
        GameObject enemyDropManaget = GameObject.Find("DropItemManager");
        enemyDropManaget.GetComponent<DropItemManager>().EnemyDropItem(transform.position , id);
        Destroy(gameObject);
    }



}
