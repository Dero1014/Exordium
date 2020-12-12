using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Permament Item", menuName = "InventorySystems/Items/Permament")]
public class Permament_Item : Item_Base_Object
{
    private void Awake()
    {
        type = ItemType.Permanent;
        stack = StackType.StackMax;
    }

}
