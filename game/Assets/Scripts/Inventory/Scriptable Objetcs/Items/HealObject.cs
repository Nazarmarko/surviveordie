using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Heal Object", menuName = "InventorySystem/Item/Heal")]
public class HealObject : ItemObject
{
    public void Awake()
    {
       type = ItemType.Heal;
    }
}
