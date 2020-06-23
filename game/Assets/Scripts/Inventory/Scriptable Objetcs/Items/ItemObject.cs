﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Heal, Equipment, Default }
public enum Attributes { Stamina, Health, Strength, Starve }
public class ItemObject : ScriptableObject
{
    public int ID;
    public Sprite uiDisplay;
    public ItemType type;

    [TextArea(15, 20)]
    public string descriptionText;

    public ItemBuff[] buffs;

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
    public int ID;

    public ItemBuff[] buffs;
    public Item(ItemObject item)
    {
        Name = item.name;
        ID = item.ID;
        buffs = new ItemBuff[item.buffs.Length];

        for (int i = 0; i < buffs.Length; i++)
        {
            buffs[i] = new ItemBuff(item.buffs[i].min, item.buffs[i].max);
            buffs[i].attribute = item.buffs[i].attribute;
        }
    }
}

[System.Serializable]
public class ItemBuff
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
}