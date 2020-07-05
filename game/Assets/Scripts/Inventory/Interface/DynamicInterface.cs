using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject InventorySlotPrefab;

    public int xSpaceBTWNItems, ySpaceBTWNItems, numberOfColumn, xStart, yStart;
    private Vector3 GetPositiion(int i)
    {
        return new Vector3(xStart + (xSpaceBTWNItems * (i % numberOfColumn)),
            yStart + (-ySpaceBTWNItems * (i / numberOfColumn)), 0f);
    }
    public override void CreateSlots()
    {
        slotsOnIterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(InventorySlotPrefab, Vector3.zero, Quaternion.identity, transform);

            obj.GetComponent<RectTransform>().localPosition = GetPositiion(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });

            slotsOnIterface.Add(obj, inventory.Container.Items[i]);
        }
    }
}
