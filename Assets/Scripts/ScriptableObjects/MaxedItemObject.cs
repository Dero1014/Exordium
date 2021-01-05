using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Max Item", menuName = "InventorySystems/Items/Max")]
public class MaxedItemObject : ItemBaseObject
{

    private void Awake()
    {
        Type = ItemType.Default;
        Stack = StackType.StackMax;
    }

    

}
