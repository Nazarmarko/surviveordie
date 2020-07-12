using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Database", menuName = "InventorySystem/Item/Database")]
public class ItemDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public ItemObject[] ItemObjects;
    public Dictionary<ItemObject, int> GetID = new Dictionary<ItemObject, int>();


    public void OnAfterDeserialize()
    {
        for (int i = 0; i < ItemObjects.Length; i++)
        {
            ItemObjects[i].data.ID = i;
        }
    }

    public void OnBeforeSerialize()
    {

    }
}
