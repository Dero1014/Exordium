using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemComponent : MonoBehaviour
{

    public ItemBaseObject ItemObject;
    public int Amount = 1;
    public int Durrability = 0;

    //private
    private TextMeshPro _amountText;
    private SpriteRenderer _sprite;


    private void Start()
    {
        _sprite = gameObject.GetComponentInChildren<SpriteRenderer>();

        _sprite.sprite = ItemObject.Sprite;

        if (gameObject.GetComponentInChildren<TextMeshPro>())
        {
            _amountText = gameObject.GetComponentInChildren<TextMeshPro>();
            _amountText.text = Amount.ToString();
        }
       
    }

}
