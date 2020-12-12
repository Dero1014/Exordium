using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Max Item", menuName = "InventorySystems/Items/Max")]
public class Maxed_Item_Object : Item_Base_Object
{

    private void Awake()
    {
        maxItemCount = 1;
        type = ItemType.Default;
        stack = StackType.StackMax;
    }

    

}
