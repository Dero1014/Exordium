using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "InventorySystems/Items/Equip")]
public class Equip_Item_Object : Item_Base_Object
{

    private void Awake()
    {
        maxItemCount = 1;
        type = ItemType.Equipable;
        stack = StackType.StackMax;
        equipType = EquipType.None;
    }

}
