using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equip Item", menuName = "InventorySystems/Items/Equip")]
public class EquipItemObject : ItemBaseObject
{

    private void Awake()
    {
        MaxDurrability = 100;
        MaxItemCount = 1;
        Type = ItemType.Equipable;
        Stack = StackType.StackMax;
    }

}
