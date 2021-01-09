using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Permament Item", menuName = "InventorySystems/Items/Permament")]
public class PermamentItem : ItemBaseObject
{
    private void Awake()
    {
        Type = ItemType.Permanent;
        Stack = StackType.StackMax;
    }

}
