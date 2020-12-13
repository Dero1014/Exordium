using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attributes : MonoBehaviour
{

    public Inventory_Object equipment;
    [HideInInspector]public List<Item_Base_Object> pickedUpItems = new List<Item_Base_Object>();
    public int[] attributes = {0, 0, 0, 0}; //0- strength, 1- dexterity, 2- agility, 3- inteligence

    private int[] attributesSaved = { 0, 0, 0, 0 };
    
    void Start()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributesSaved[i] = attributes[i];
        }
    }

    //attributes[(int)equipment.container[i].item.buffs[j].attribute] = attributes[(int)equipment.container[i].item.buffs[j].attribute] + equipment.container[i].item.buffs[j].value;

    void Update()
    {
        //apply the stats from equipment to the attributes
        //add them with existing values of that attribute
        //

        int[] atrNums = { 0, 0, 0, 0 };
        

        for (int i = 0; i < equipment.container.Count; i++)
        {
            if (equipment.container[i].item)
            {
                if (equipment.container[i].item.buffs.Length > 0)
                {
                    for (int j = 0; j < equipment.container[i].item.buffs.Length; j++)
                    {
                        atrNums[(int)equipment.container[i].item.buffs[j].attribute] += equipment.container[i].item.buffs[j].value;
                    }
                }
            }
        }

        for (int i = 0; i < pickedUpItems.Count; i++)
        {
            for (int j = 0; j < pickedUpItems[i].buffs.Length; j++)
            {
                atrNums[(int)pickedUpItems[i].buffs[j].attribute] += pickedUpItems[i].buffs[j].value;
            }
        }

        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i] = atrNums[i];
        }


    }

}
