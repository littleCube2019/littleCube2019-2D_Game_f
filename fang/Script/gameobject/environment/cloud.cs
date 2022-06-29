using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloud : MonoBehaviour
{
    // Start is called before the first frame update
    public float rainingDense = 10f;
    public float rainingProb  = 1f;
    public float rainingPeriod = 2f;
    public float sunnyPeriod = 2f;

    IEnumerator raining;


    IEnumerator Raining(){
        while(true){
            GameObject new_drip = Instantiate(transform.GetChild(0).gameObject  , transform); 

            float width = GetComponent<SpriteRenderer>().bounds.size.x;
            float offset = Random.Range(-width/2 ,width/2 );
            new_drip.transform.position = transform.position +  Vector3.right * offset ;
        
            new_drip.GetComponent<Rigidbody2D>().gravityScale = 1;
            yield return new WaitForSeconds(1f/rainingDense);

        }
    }
    void Start()
    {   
        
        StartCoroutine(Cloud());
    }

    public void rainingNow(){
        StartCoroutine(RainingNow());
    }

    IEnumerator RainingNow(){
        IEnumerator tmp = Raining();
        StartCoroutine(tmp);
        yield return new WaitForSeconds( rainingPeriod );
        StopCoroutine(tmp);
    }

    IEnumerator Cloud(){
        while(true){
            float r = Random.value;
            if(rainingProb >= r+0.000001f){
                raining = Raining();
                StartCoroutine(raining);
                yield return new WaitForSeconds( rainingPeriod );
                StopCoroutine(raining);   
            }
            else{
                yield return new WaitForSeconds( rainingPeriod );
            }
        }
    }
    
    
}
