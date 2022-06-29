using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class status : MonoBehaviour
{
    // Start is called before the first frame update
    
    [SerializeField] int maxHp = 100 ; 
    [SerializeField] int Hp = 100 ; 
    [SerializeField] Gradient hpGradient ; 
    Slider hpBar ; 
    
    Image hpFill ; 
    public void setMaxHp(int v){
        maxHp = v;
        Hp = v;
        hpBar.value = v;
        hpFill.color = hpGradient.Evaluate(1f);
    }

    public void takeDamage(int v){
        v = Mathf.Min(Hp,v);
        Hp -= v;
        
        hpBar.value = Hp;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }
    
    public void takeHeal(int v){
        Hp += v;
        Hp = Mathf.Min(Hp , maxHp);
        hpBar.value = Hp;
        hpFill.color = hpGradient.Evaluate(hpBar.normalizedValue);
    }


    void Start()
    {
        GameObject hpBarObj = GameObject.Find("hp_bar");

        hpBar = hpBarObj.GetComponent<Slider>();
       
        hpFill = hpBarObj.transform.GetChild(0).GetComponent<Image>();
        hpBar.maxValue = maxHp;
        hpBar.value = Hp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
