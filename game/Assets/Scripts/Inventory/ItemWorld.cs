﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item) 
    {
       Transform transform = Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

       ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);
        return itemWorld;
    }

    public static ItemWorld DropItem(Item item) 
    {
    SpawnItemWorld()
    }

    private Item item;
    private SpriteRenderer spriteRenderer;
    private TextMeshPro textMeshPro;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>(); 
    }
    public void SetItem(Item item) 
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else 
        {
            textMeshPro.SetText("");
        }

    }
    public Item GetItem() 
    {
        return item;
    }

    public void DestroySelf() 
    {
        Destroy(gameObject);
    }

}
