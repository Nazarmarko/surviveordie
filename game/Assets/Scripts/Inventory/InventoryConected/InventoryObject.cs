using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;
using System.Runtime.Serialization;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class InventoryObject : ScriptableObject
{
    public string safePath;
    public ItemDatabase database;
    public Inventory Container;

    public void AddItem(ItemObject _item, int _amount)
    {
        if (_item.buffs.Length > 0)
        {
            Container.Items.Add(new InventorySlot(database.GetID[_item], _item, _amount));
            return;
        }

        for (int i = 0; i < Container.Items.Count; i++)
        {
            if (Container.Items[i].item.ID == _item.ID)
            {
                Container.Items[i].AddAmount(_amount);
                return;
            }
        }
        Container.Items.Add(new InventorySlot(database.GetID[_item], _item, _amount));
    }
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

            Container = (Inventory)formatter.Deserialize(stream);

            stream.Close();
        }
    }
    [ContextMenu("Clear")]
    public void Clear()
    {
        Container = new Inventory();
    }
}
[System.Serializable]
public class Inventory
{
    public List<InventorySlot> Items = new List<InventorySlot>();
}
[System.Serializable]
public class InventorySlot
{
    public int ID, amount;
    public ItemObject item;

    public InventorySlot(int _id, ItemObject _item, int _amount)
    {
        ID = _id;
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
