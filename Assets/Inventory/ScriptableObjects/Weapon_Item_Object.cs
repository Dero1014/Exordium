using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Item", menuName = "InventorySystems/Items/Weapon")]
public class Weapon_Item_Object : Item_Base_Object
{

    private void Awake()
    {
        type = ItemType.Weapon;
        stack = StackType.StackMax;
    }

}
