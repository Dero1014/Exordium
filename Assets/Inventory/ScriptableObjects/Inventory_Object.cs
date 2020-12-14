using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "InventorySystems/Inventory")]
public class Inventory_Object : ScriptableObject
{
    public int capacity;
    [Space(10)]
    public List<Inventory_Slot> container = new List<Inventory_Slot>();

    public void Awake()
    {
        container.Capacity = capacity;
    }

    public void AddItem(Item_Base_Object item, int amount)
    {
        if (item.stack == StackType.StackInf)
        {

            for (int i = 0; i < container.Count; i++)
            {
                if (container[i].item == item)
                {
                    container[i].AddAmount(amount);
                    return;
                }
            }

            container.Add(new Inventory_Slot(item, amount));

        }
        else if (item.stack == StackType.StackMax)
        {

            for (int i = 0; i < container.Count; i++)
            {

                if (container[i].item == item)
                {
                    if (container[i].amount < container[i].item.maxItemCount )
                    {
                        if (container[i].amount + amount <= item.maxItemCount)
                        {
                            container[i].AddAmount(amount);
                            return;
                        }
                        else
                        {
                            int leftOver = item.maxItemCount - container[i].amount;
                            container[i].AddAmount(leftOver);
                            amount -= leftOver;
                        }

                    }
                }
            }

            if (amount <= item.maxItemCount)
            {
                container.Add(new Inventory_Slot(item, amount));
            }
            else
            {
                while (amount > item.maxItemCount)
                {
                    container.Add(new Inventory_Slot(item, item.maxItemCount));
                    amount -= item.maxItemCount;
                }

                container.Add(new Inventory_Slot(item, amount));
            }


        }
    }

}

[System.Serializable]
public class Inventory_Slot
{

    public Item_Base_Object item;
    public EquipType[] allowedEquip;
    public int amount;

    public Inventory_Slot(Item_Base_Object itemType, int amountItem)
    {
        amount = amountItem;
        item = itemType;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }

}
