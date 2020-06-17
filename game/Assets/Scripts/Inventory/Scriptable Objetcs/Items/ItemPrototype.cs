using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Heal, Equipment, Default }
public abstract class ItemPrototype : ScriptableObject
{
    public GameObject prefab;
    public ItemType type;

    [TextArea(15,20)]
    public string descriptionText;
}
