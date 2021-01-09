using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType //types of items
{
    Equipable,
    Default,
    Permanent,
}

public enum EquipType //types of items
{
    None,
    Hands,
    Shield,
    Head,
    Chest,
    Boots,
    Ring
}

public enum StackType
{
    StackMax,
    StackInf,
}

public enum AttributesType
{
    Strength,
    Dexterity,
    Agility,
    Inteligence,
    Luck,
    Health,
    Mana
}

public abstract class ItemBaseObject : ScriptableObject
{
    public string ItemName;
    [Space(10)]
    public GameObject Prefab; //holds the graphic of the item
    public Sprite Sprite;
    public ItemType Type;
    public EquipType EquipTypes;
    public StackType Stack;
    public int MaxItemCount;
    [TextArea(2, 15)]
    public string Description;
    public ItemBuff[] Buffs;
    [Space(10)]
    public bool HoldValue;
    public float HoldTime;
    public bool RampValue;
    public float rampItUpTime;
    public bool ValueOverTime;
    public float addValueOverTime;
}

[System.Serializable]
public class ItemBuff
{

    public AttributesType Attribute;
    public int Value;

    
    
    public ItemBuff(int _value)
    {
        Value = _value;
    }

}
