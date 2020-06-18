using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "InventorySystem/Item/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemPrototype[] itemsToUpload;
    public Dictionary<ItemPrototype, int> GetID = new Dictionary<ItemPrototype, int>();
    public Dictionary<int, ItemPrototype> GetItem = new Dictionary<int, ItemPrototype>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<ItemPrototype, int>();
        GetItem = new Dictionary<int, ItemPrototype>();

        for (int i = 0; i < itemsToUpload.Length; i++)
        {
            GetID.Add(itemsToUpload[i], i);

            GetItem.Add(i, itemsToUpload[i]);
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
