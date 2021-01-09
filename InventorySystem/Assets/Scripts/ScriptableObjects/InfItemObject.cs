using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inf Item", menuName = "InventorySystems/Items/Inf")]
public class InfItemObject : ItemBaseObject
{

    private void Awake()
    {
        Type = ItemType.Default;
        Stack = StackType.StackInf;
    }
}
