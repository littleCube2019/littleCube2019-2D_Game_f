using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DroppedItem : MonoBehaviour
{
    private Item item;
    private bool playerIn;
    private Transform player;
    private IEnumerator Start()
    {
        playerIn = false;
        yield return new WaitForSeconds(120);
        Destroy(gameObject);
    }
    private void Update()
    {
        if (playerIn)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.GetComponent<UI_Player>().inventory.AddItem(item);
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "main_character")
        {
            return;
        }
        player = collision.transform;
        playerIn = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name != "main_character")
        {
            return;
        }
        playerIn = false;
    }
    public void SetItem(Item item)
    {
        this.item = item;
        Transform image = transform.Find("image");
        image.GetComponent<SpriteRenderer>().sprite = item.GetSprite();
    }
}
