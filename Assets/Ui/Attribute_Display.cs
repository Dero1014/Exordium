using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Attribute_Display : MonoBehaviour
{
    public TextMeshProUGUI[] attributes; //0- strength, 1- dexterity, 2- agility, 3- inteligence

    private Player_Attributes pAttributes;

    void Start()
    {
        pAttributes = GameObject.FindObjectOfType<Player_Attributes>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].text = pAttributes.attributes[i].ToString();
        }
    }
}
