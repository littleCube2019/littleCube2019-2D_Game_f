using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    private GameObject target;
    private void Awake()
    {
        target = GameObject.Find("main_character");
 
    }
    void LateUpdate() 
    {
        if (transform.position != target.transform.position)
        {
            Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
          

            transform.position = targetPosition;

        }
    }
}
