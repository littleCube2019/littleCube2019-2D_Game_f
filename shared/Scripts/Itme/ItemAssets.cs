using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour
{
    public static ItemAssets Instance { get; private set; }
    private void Awake()
    {
        Instance = this;
    }


    public List<item_obj> Info;

    //public Sprite wood;
    //public Sprite woodElement;
    //public Sprite magicStick;
 
}
