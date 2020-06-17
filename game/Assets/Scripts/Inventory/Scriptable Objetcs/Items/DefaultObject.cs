using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Object", menuName = "InventorySystem/Item/Default")]
public class DefaultObject : ItemPrototype
{
    public void Awake()
    {
        type = ItemType.Default;
    }
}

