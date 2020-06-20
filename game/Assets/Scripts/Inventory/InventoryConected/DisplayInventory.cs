using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;

    public int xSpaceBTWNItems, ySpaceBTWNItems, numberOfColumn, xStart, yStart;

    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

    public Vector3 GetPositiion(int i)
    {
        return new Vector3(xStart + (xSpaceBTWNItems * (i % numberOfColumn)), yStart + (-ySpaceBTWNItems * (i / numberOfColumn)), 0f);
    }
    void Awake()
    {
        CreateDisplay();
    }
    public void CreateDisplay()
    { 
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPositiion(i);
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

            itemsDisplayed.Add(slot, obj);
        }
    }  
    void Update()
    {
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];

            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text 
                    = slot.amount.ToString("n0");
            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);

                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite
                    = inventory.database.GetItem[slot.item.ID].uiDisplay;

                obj.GetComponent<RectTransform>().localPosition = GetPositiion(i);

                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

                itemsDisplayed.Add(inventory.Container.Items[i], obj);
            }
        } 
    }
}
