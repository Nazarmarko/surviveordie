using UnityEngine.UI;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;

    private Sprite slotSprite;
    private void OnEnable()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D inventoryCol)
    {
        if (inventoryCol.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slotInInventory.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    inventory.isFull[i] = true;

                    slotSprite = this.gameObject.GetComponent<SpriteRenderer>().sprite;


                    inventory.slotInInventory[i].gameObject.GetComponent<Image>().sprite = slotSprite;

                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
}
