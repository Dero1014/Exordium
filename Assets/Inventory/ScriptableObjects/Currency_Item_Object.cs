using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Currency Item", menuName = "InventorySystems/Items/Currency")]
public class Currency_Item_Object : Item_Base_Object
{

    private void Awake()
    {
        type = ItemType.Default;
        stack = StackType.StackInf;
    }
}
