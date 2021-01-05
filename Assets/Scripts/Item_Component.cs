using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item_Component : MonoBehaviour
{

    public Item_Base_Object itemType;
    public int amount = 1;

    //private
    private TextMeshPro amountText;
    private SpriteRenderer sprite;


    private void Start()
    {
        sprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        sprite.sprite = itemType.sprite;

        if (gameObject.GetComponentInChildren<TextMeshPro>())
        {
            amountText = gameObject.GetComponentInChildren<TextMeshPro>();
            amountText.text = amount.ToString();
        }
       
    }

}
