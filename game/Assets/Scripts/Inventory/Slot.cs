using UnityEngine.UI;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    private Image currentImage;

    public int boolIndex;
     void OnEnable()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
        currentImage = GetComponent<Image>();
        currentImage.sprite = null;
    }
    private void Update()
    {
        if(currentImage.sprite == null)
        {
            inventory.isFull[boolIndex] = false;
        }
    }
    public void DropItem()
    {
            this.gameObject.GetComponent<SpawnItem>().SpawnDroppedItem();
            currentImage.sprite = null;
    }
}
