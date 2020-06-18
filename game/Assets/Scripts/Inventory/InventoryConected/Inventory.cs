using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEditor;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystem/Inventory")]
public class Inventory : ScriptableObject, ISerializationCallbackReceiver
{
    public string safePath;
    private ItemDatabase database;
    public List<InventorySlot> Container = new List<InventorySlot>();

    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (ItemDatabase)AssetDatabase.LoadAssetAtPath
            ("Assets/Scripts/Inventory/Scriptable Objects/Resources/Database.asset", typeof(ItemDatabase));
#else
        database = Resources.Load<ItemDatabase>("Database");
#endif
    }
    public void AddItem(ItemPrototype _item, int _amount)
    {
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item)
            {
                Container[i].AddAmount(_amount);
                return;
            }
        }
        Container.Add(new InventorySlot(database.GetID[_item], _item, _amount));
    }
    public void Save()
    {
        string saveData = JsonUtility.ToJson(this, true);
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(string.Concat(Application.persistentDataPath, safePath));
        bf.Serialize(file, saveData);
        file.Close();

    }
    public void Load()
    {
        if (File.Exists(string.Concat(Application.persistentDataPath, safePath)))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(string.Concat(Application.persistentDataPath, safePath), FileMode.Open);
            JsonUtility.FromJsonOverwrite(bf.Deserialize(file).ToString(), this);
            file.Close();
        }
    }
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < Container.Count; i++)
            Container[i].item = database.GetItem[Container[i].ID];
    }

    public void OnBeforeSerialize()
    {

    }
}
[System.Serializable]
public class InventorySlot
{
    public int ID;
    public ItemPrototype item;
    public int amount;
    public InventorySlot(int _id, ItemPrototype _item, int _amount)
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
