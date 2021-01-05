using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemComponent : MonoBehaviour
{

    public ItemBaseObject ItemType;
    public int Amount = 1;

    //private
    private TextMeshPro _amountText;
    private SpriteRenderer _sprite;


    private void Start()
    {
        _sprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        _sprite.sprite = ItemType.Sprite;

        if (gameObject.GetComponentInChildren<TextMeshPro>())
        {
            _amountText = gameObject.GetComponentInChildren<TextMeshPro>();
            _amountText.text = Amount.ToString();
        }
       
    }

}
