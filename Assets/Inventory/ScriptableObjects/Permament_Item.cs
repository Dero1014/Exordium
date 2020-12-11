using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permament_Item : Item_Base_Object
{
    private void Awake()
    {
        type = ItemType.Permanent;
        stack = StackType.StackMax;
    }

}
