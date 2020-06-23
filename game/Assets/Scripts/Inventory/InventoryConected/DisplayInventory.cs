using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DisplayInventory : MonoBehaviour
{
    public GameObject InventorySlotPrefab;
    public InventoryObject inventory;

    public int xSpaceBTWNItems, ySpaceBTWNItems, numberOfColumn, xStart, yStart;

    Dictionary<GameObject, InventorySlot> itemsDisplayed = new Dictionary<GameObject, InventorySlot>();

    public Vector3 GetPositiion(int i)
    {
        return new Vector3(xStart + (xSpaceBTWNItems * (i % numberOfColumn)),
            yStart + (-ySpaceBTWNItems * (i / numberOfColumn)), 0f);
    }
    void Awake()
    {
        CreateSlots();
    }
    public void CreateSlots()
    {
        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);

            obj.GetComponent<RectTransform>().localPosition = GetPositiion(i);

            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }
    void Update()
    {
        UpdateDisplay();
    }
    public void UpdateDisplay()
    {

    }
}
