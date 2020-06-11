using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;

    public GameObject itemButtom;
    private void OnEnable()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D invetoryCol)
    {
        if (invetoryCol.CompareTag("Inventory"))
        {
            for (int i = 0; i < inventory.slotInInventory.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;

                    Instantiate(itemButtom, inventory.slotInInventory[i].transform);

                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
