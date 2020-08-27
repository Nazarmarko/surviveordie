using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Heal, Helmet, Weapon, Shields, Boots, Chest, Tool, Default }
public enum Attributes { Stamina, Health, Strength, Starve }
public class ItemObject : ScriptableObject
{
    public Sprite uiDisplay;
    public ItemType type;
    public bool stackable;

    [TextArea(15, 20)]
    public string descriptionText;

    public Item data = new Item();

    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}

[System.Serializable]
public class Item
{
    public string Name;
    public int ID = -1;

    public ItemBuff[] buffs;
    public Item()
    {
        Name = "";
        ID = -1;
    }
    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.data.ID;
        buffs = new ItemBuff[item.data.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.data.buffs[i].min, item.data.buffs[i].max);
            buffs[i].attribute = item.data.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff : IModifier
{
    public Attributes attribute;
    public int value, min, max;
    public ItemBuff(int _min, int _max)
    {
        min = _min;
        max = _max;
        GenerateValue();
    }

    public void GenerateValue()
    {
        value = UnityEngine.Random.Range(min, max);
    }

  public void AddValue(ref int baseValue)
    {
        baseValue += value;
    }
}
