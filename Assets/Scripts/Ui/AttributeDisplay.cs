using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AttributeDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] Attributes; //0- strength, 1- dexterity, 2- agility, 3- inteligence

    private PlayerAttributes _pAttributes;

    void Start()
    {
        for (int i = 0; i < Attributes.Length; i++)
        {
            Attributes[i].text = "0";
        }

        CustomEvents.current.OnValueChangeStr += UpdateTextStr;
        CustomEvents.current.OnValueChangeDex += UpdateTextDex;
        CustomEvents.current.OnValueChangeAgi += UpdateTextAgi;
        CustomEvents.current.OnValueChangeInt += UpdateTextInt;
        CustomEvents.current.OnValueChangeLck += UpdateTextLck;
        _pAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
    }

    void UpdateTextStr(int newValues)
    {
        Attributes[0].text = newValues.ToString();
    }
    void UpdateTextDex(int newValues)
    {
        Attributes[1].text = newValues.ToString();
    }
    void UpdateTextAgi(int newValues)
    {
        Attributes[2].text = newValues.ToString();
    }
    void UpdateTextInt(int newValues)
    {
        Attributes[3].text = newValues.ToString();
    }

    void UpdateTextLck(int newValues)
    {
        Attributes[4].text = newValues.ToString();
    }


    
}
