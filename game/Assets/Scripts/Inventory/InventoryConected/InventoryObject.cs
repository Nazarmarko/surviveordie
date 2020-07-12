using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

public enum InterfaceType { Inventory, Equipment, Chest }

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public InterfaceType type;

    public string safePath;
    public ItemDatabase database;
    public Inventory Container;
    public InventorySlot[] GetSlots { get { return Container.Slots; } }
    #region Add/Swap/Remove/SetEmpty
    public bool AddItem(Item _item, int _amount)
    {
        if (EmptySlotCount <= 0)
            return false;

        InventorySlot slot = FindItemOnInventory(_item);
        if (!database.ItemObjects[_item.ID].stackable || slot == null)
        {
            SetEmptySlot(_item, _amount);
            return true;
        }

        slot.AddAmount(_amount);
        return true;
    }
    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                if (GetSlots[i].item.ID <= -1)
                    counter++;
            }
            return counter;
        }
    }
    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.ID == _item.ID)
            {
                return GetSlots[i];
            }
        }
        return null;
    }
    public void RemoveItem(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item == _item)
            {
                GetSlots[i].UpdateSlot(null, 0);
            }
        }
    }
    public InventorySlot SetEmptySlot(Item _item, int _amount)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.ID <= -1)
            {
                GetSlots[i].UpdateSlot(_item, _amount);
                return GetSlots[i];
            }
        }
        return null;
    }
    public void SwapItem(InventorySlot item1, InventorySlot item2)
    {
        if (item2.CanPlaceInSlot(item1.ItemObject) && item1.CanPlaceInSlot(item2.ItemObject))
        {

            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
    }
    #endregion

    #region Safe/Load/Clean
    [ContextMenu("Save")]
    public void Save()
    {
        IFormatter formatter = new BinaryFormatter();
        Stream stream = new FileStream(string.Concat(Application.persistentDataPath, safePath),
            FileMode.Create, FileAccess.Write);
        formatter.Serialize(stream, Container);
        stream.Close();
    }
    [ContextMenu("Load")]
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, safePath)))
        {
            IFormatter formatter = new BinaryFormatter();

            Stream stream = new FileStream(string.Concat(Application.persistentDataPath, safePath)
                , FileMode.Open, FileAccess.Read);

            Inventory newContainer = (Inventory)formatter.Deserialize(stream);

            for (int i = 0; i < GetSlots.Length; i++)
            {
                GetSlots[i].UpdateSlot(newContainer.Slots[i].item, newContainer.Slots[i].amount);
            }

            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container.Clear();
    }
    #endregion
}
#region InventoryMehanics
[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[25];
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].RemoveItem();
        }
    }
}

public delegate void SlotUpdated(InventorySlot _slot);

[System.Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];

    public int amount;
    [System.NonSerialized]
    public UserInterface parent;
    [System.NonSerialized]
    public GameObject slotDisplay;
    [System.NonSerialized]
    public SlotUpdated OnAfterUpdate;
    [System.NonSerialized]
    public SlotUpdated OnBeforeUpdate;

    public Item item = new Item();

    public ItemObject ItemObject
    {
        get
        {
            if (item.ID >= 0)
            {
                return parent.inventory.database.ItemObjects[item.ID];
            }
            return null;
        }
    }
    public InventorySlot()
    {
        UpdateSlot(new Item(), 0);
    }
    public InventorySlot(Item _item, int _amount)
    {
        UpdateSlot(_item, _amount);
    }
    public void UpdateSlot(Item _item, int _amount)
    {
        if (OnBeforeUpdate != null)
            OnBeforeUpdate.Invoke(this);

        item = _item;
        amount = _amount;

        if (OnAfterUpdate != null)
            OnAfterUpdate.Invoke(this);
    }
    public void RemoveItem()
    {
        UpdateSlot(new Item(), 0);
    }
    public void AddAmount(int value)
    {
        UpdateSlot(item, amount += value);
    }
    public bool CanPlaceInSlot(ItemObject _itemObject)
    {
        if (AllowedItems.Length <= 0 || _itemObject == null || _itemObject.data.ID < 0)
            return true;

        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (_itemObject.type == AllowedItems[i])
                return true;
        }
        return false;
    }
    #endregion
}
