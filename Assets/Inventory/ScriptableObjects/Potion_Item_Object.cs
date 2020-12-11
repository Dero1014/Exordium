using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion Item", menuName = "InventorySystems/Items/Potion")]
public class Potion_Item_Object : Item_Base_Object
{

    private void Awake()
    {
        type = ItemType.Potion;
        stack = StackType.StackMax;
    }

    

}
