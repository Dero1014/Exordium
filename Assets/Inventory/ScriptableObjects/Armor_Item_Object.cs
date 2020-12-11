using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor Item", menuName = "InventorySystems/Items/Armor")]
public class Armor_Item_Object : Item_Base_Object
{

    private void Awake()
    {
        type = ItemType.Armor;
        stack = StackType.StackMax;
    }
}
