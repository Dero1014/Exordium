using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxyItemPicker : MonoBehaviour
{

    //public
    public LayerMask Mask;
    public float PickUpRadius;


    //private
    private PlayerPickUp _pickUp;


    void Start()
    {
        _pickUp = GameObject.FindObjectOfType<PlayerPickUp>();
    }


    private void FixedUpdate()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, PickUpRadius, Mask);

        foreach (Collider2D c in col)
        {
            if (c.GetComponent<ItemComponent>())
            {
                //here we update to inventory Ui
                ItemComponent item = c.transform.gameObject.GetComponent<ItemComponent>();
                _pickUp.PickedUp(item);

            }
        } 

    }

}
