using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystems/Inventory")]
public class InventoryObject : ScriptableObject
{
    public int Capacity;
    [Space(10)]
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void Awake()
    {
        Container.Capacity = Capacity;
    }

    public void AddItem(ItemBaseObject item, int amount, int durrability)
    {
        if (item.Stack == StackType.StackInf)
        {

            for (int i = 0; i < Container.Count; i++)
            {
                if (Container[i].Item == item)
                {
                    Container[i].AddAmount(amount);
                    return;
                }
            }

            Container.Add(new InventorySlot(item, amount, durrability));

        }
        else if (item.Stack == StackType.StackMax)
        {

            for (int i = 0; i < Container.Count; i++)
            {

                if (Container[i].Item == item)
                {
                    if (Container[i].Amount < Container[i].Item.MaxItemCount )
                    {
                        if (Container[i].Amount + amount <= item.MaxItemCount)
                        {
                            Container[i].AddAmount(amount);
                            return;
                        }
                        else
                        {
                            int leftOver = item.MaxItemCount - Container[i].Amount;
                            Container[i].AddAmount(leftOver);
                            amount -= leftOver;
                        }

                    }
                }
            }

            if (amount <= item.MaxItemCount)
            {
                Container.Add(new InventorySlot(item, amount, durrability));
            }
            else
            {
                while (amount > item.MaxItemCount)
                {
                    Container.Add(new InventorySlot(item, item.MaxItemCount, durrability));
                    amount -= item.MaxItemCount;
                }

                Container.Add(new InventorySlot(item, amount, durrability));
            }


        }
    }

    public void SplitItems(InventorySlot containerIndex, int splitAmount, int durrability)
    {
        ItemBaseObject item = containerIndex.Item;
        Container[Container.IndexOf(containerIndex)].Amount -= splitAmount;

        Container.Add(new InventorySlot(item, splitAmount, durrability));

    }
}

[System.Serializable]
public class InventorySlot
{

    public ItemBaseObject Item;
    public EquipType AllowedEquip;
    public int Amount;
    public int Durrability;

    public InventorySlot(ItemBaseObject itemType, int amountItem, int durrabilityItem)
    {
        Amount = amountItem;
        Item = itemType;
        Durrability = durrabilityItem;
    }

    public void AddAmount(int value)
    {
        Amount += value;
    }

}
