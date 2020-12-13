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
}

public enum StackType
{
    StackMax,
    StackInf,
}

public enum Attributes
{
    Strength,
    Dexterity,
    Agility,
    Inteligence,
}

public abstract class Item_Base_Object : ScriptableObject
{
    public string itemName;
    public int id = 0;
    [Space(10)]
    public GameObject prefab; //holds the graphic of the item
    public ItemType type;
    public EquipType equipType;
    public StackType stack;
    public int maxItemCount;
    [TextArea(2, 15)]
    public string description;

    public ItemBuff[] buffs;

}

[System.Serializable]
public class ItemBuff
{

    public Attributes attribute;
    public int value;
    
    public ItemBuff(int _value)
    {
        value = _value;
    }

}
