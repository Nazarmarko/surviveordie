using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public abstract class UserInterface : MonoBehaviour
{
    #region Variables
    public InventoryObject inventory;

    public Dictionary<GameObject, InventorySlot> slotsOnIterface = new Dictionary<GameObject, InventorySlot>();
    #endregion

    #region InventorySetUp
    void Awake()
    {
        slotsOnIterface.UpdateSlotDisplay();

        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            Debug.Log("1");
            inventory.Container.Items[i].parent = this;
        }

        CreateSlots();


        AddEvent(gameObject, EventTriggerType.PointerEnter, delegate { OnEnterInterface(gameObject); });
        AddEvent(gameObject, EventTriggerType.PointerExit, delegate { OnExitInterface(gameObject); });
    }
    public abstract void CreateSlots();

    #endregion

    #region EventSystem
    protected void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();

        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);
    }
    public void OnEnter(GameObject obj)
    {
        MouseData.slotHoveredOver = obj;
    }
    public void OnExit(GameObject obj)
    {
        MouseData.slotHoveredOver = null;
    }
    public void OnEnterInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = obj.GetComponent<UserInterface>();
    }
    public void OnExitInterface(GameObject obj)
    {
        MouseData.interfaceMouseIsOver = null;
    }
    public void OnDragStart(GameObject obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }
    public GameObject CreateTempItem(GameObject obj)
    {
        GameObject tempItem = null;
        if (slotsOnIterface[obj].item.ID >= 0)
        {
            tempItem = new GameObject();

            var rt = tempItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(200, 200);

            tempItem.transform.SetParent(transform.parent);

            var img = tempItem.AddComponent<Image>();
            img.sprite = slotsOnIterface[obj].ItemObject.uiDisplay;
            img.raycastTarget = false;
        }
        return tempItem;
    }
    public void OnDragEnd(GameObject obj)
    {
        Destroy(MouseData.tempItemBeingDragged);

        if (MouseData.interfaceMouseIsOver == null)
        {
            slotsOnIterface[obj].RemoveItem();
            return;
        }
        if (MouseData.slotHoveredOver)
        {
            InventorySlot mouseHoverSlotData = MouseData.interfaceMouseIsOver.slotsOnIterface[MouseData.slotHoveredOver];
            inventory.SwapItem(slotsOnIterface[obj], mouseHoverSlotData);
        }
    }
    public void OnDrag(GameObject obj)
    {
        if (MouseData.tempItemBeingDragged != null)
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Input.mousePosition;
    }
    #endregion

    #region UpdateInventory
    void Update()
    {
        slotsOnIterface.UpdateSlotDisplay();
    }
    #endregion
}
public static class MouseData
{
    public static UserInterface interfaceMouseIsOver;

    public static GameObject tempItemBeingDragged, slotHoveredOver;
}
public static class ExtensioNmETHOD
{
    public static void UpdateSlotDisplay(this Dictionary<GameObject, InventorySlot> _slotsOnInterface)
    {
        foreach (KeyValuePair<GameObject, InventorySlot> _slot in _slotsOnInterface)
        {
            if (_slot.Value.item.ID >= 0)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite
                    = _slot.Value.ItemObject.uiDisplay;

                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);

                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text
                    = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;

                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);

                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }
    }
}

