using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon_manager : MonoBehaviour
{
    // Start is called before the first frame update
    bool isUsing = false;
    [SerializeField] List<GameObject> weapons ;
    public enum weaponID
    {
        spear,

    }


      IEnumerator MoveToward(GameObject weapon){
        int t = 0;
        
        readWeaponInfo info = weapon.GetComponent<readWeaponInfo>();
        
        
        while(t < 10){
            if(info == null){
                Debug.Log("Bad things happened at near weapon_manager.cs line 25");
                break; //bad 
            }
            weapon.transform.Translate(new Vector3( 0.05f  , 0  , 0));
           

            yield return new WaitForSeconds(1/info.attack_speed);
            t+=1;
        }
        
        weapon.transform.localPosition = new Vector3(-0.1f, 0, 0);
        //weapon.SetActive(false);
        isUsing = false;
        Destroy(weapon);
    }


    void Start()
    {
        
    }

    public void usage(int id, Vector2 mousePos , int faceToRight=1 ,int mouseAtRight=1  ){
        if(!isUsing){
            isUsing = true;

            GameObject weapon = Instantiate(weapons[id] , transform.parent);
            weapon.transform.localScale = Vector3.Scale(transform.localScale ,weapon.transform.localScale);
            if(id == (int)weapon_manager.weaponID.spear){
                
                weapon.transform.position = transform.position;
                StartCoroutine(MoveToward(weapon));
            }
        }
    }
}
