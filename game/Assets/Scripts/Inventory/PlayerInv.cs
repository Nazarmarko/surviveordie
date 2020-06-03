using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        //ItemWorld.SpawnItemWorld(new Vector3(1, 1), new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(-1, 1), new Item { itemType = Item.ItemType.ManaPotion, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(0, 0), new Item { itemType = Item.ItemType.Sword, amount = 1 });
        //ItemWorld.SpawnItemWorld(new Vector3(-1, -1), new Item { itemType = Item.ItemType.Coin, amount = 1 });
    }

    private void OnTriggerEnter2D(Collider2D collider) //Touching item
    {
        ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
        if (itemWorld != null) 
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }
}

