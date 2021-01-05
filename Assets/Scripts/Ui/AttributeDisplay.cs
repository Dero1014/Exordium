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
        _pAttributes = GameObject.FindObjectOfType<PlayerAttributes>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Attributes.Length; i++)
        {
            Attributes[i].text = _pAttributes.Attributes[i].ToString();
        }
    }

    public void UpdateText()
    {
        print("It works!");
    }
}
