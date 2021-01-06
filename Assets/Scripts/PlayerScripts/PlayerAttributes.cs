using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{

    public InventoryObject Equipment;
    [HideInInspector]public List<ItemBaseObject> PickedUpItems = new List<ItemBaseObject>();
    public int[] Attributes = {0, 0, 0, 0}; //0- strength, 1- dexterity, 2- agility, 3- inteligence

    private int[] AttributesSaved = { 0, 0, 0, 0 };

    void Start()
    {
        for (int i = 0; i < Attributes.Length; i++)
        {
            AttributesSaved[i] = Attributes[i];
        }
    }

    //changing the script to function with OnChangeTrigger
    //this will happen whenever something has been equiped or dequiped as well as change from perm items

    void Update()
    {
        //apply the stats from equipment to the attributes
        //add them with existing values of that attribute

        int[] atrNums = { 0, 0, 0, 0 };
        

        for (int i = 0; i < Equipment.Container.Count; i++)
        {
            if (Equipment.Container[i].Item)
            {
                if (Equipment.Container[i].Item.Buffs.Length > 0)
                {
                    for (int j = 0; j < Equipment.Container[i].Item.Buffs.Length; j++)
                    {
                        atrNums[(int)Equipment.Container[i].Item.Buffs[j].Attribute] += Equipment.Container[i].Item.Buffs[j].Value;
                    }
                }
            }
        }

        for (int i = 0; i < PickedUpItems.Count; i++)
        {
            for (int j = 0; j < PickedUpItems[i].Buffs.Length; j++)
            {
                atrNums[(int)PickedUpItems[i].Buffs[j].Attribute] += PickedUpItems[i].Buffs[j].Value;
            }
        }

        for (int i = 0; i < Attributes.Length; i++)
        {
            Attributes[i] = atrNums[i];
        }

        CustomEvents.current.StrValue = atrNums[0];
        CustomEvents.current.DexValue = atrNums[1];
        CustomEvents.current.AgiValue = atrNums[2];
        CustomEvents.current.IntValue = atrNums[3];
    }

}
