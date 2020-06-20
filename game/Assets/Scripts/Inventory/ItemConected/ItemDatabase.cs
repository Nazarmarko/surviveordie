using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "InventorySystem/Item/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] itemsToUpload;
    public Dictionary<ItemObject, int> GetID = new Dictionary<ItemObject, int>();
    public Dictionary<int, ItemObject> GetItem = new Dictionary<int, ItemObject>();

    public void OnAfterDeserialize()
    {
        GetID = new Dictionary<ItemObject, int>();
        GetItem = new Dictionary<int, ItemObject>();

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
