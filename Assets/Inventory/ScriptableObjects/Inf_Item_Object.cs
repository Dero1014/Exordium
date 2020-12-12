using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inf Item", menuName = "InventorySystems/Items/Inf")]
public class Inf_Item_Object : Item_Base_Object
{

    private void Awake()
    {
        type = ItemType.Default;
        stack = StackType.StackInf;
    }
}
