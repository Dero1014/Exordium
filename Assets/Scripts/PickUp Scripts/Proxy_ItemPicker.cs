using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proxy_ItemPicker : MonoBehaviour
{

    //public

    public LayerMask mask;
    public float pickUpRadius;



    //private

    private Player_PickUp pickUp;

    void Start()
    {
        pickUp = GameObject.FindObjectOfType<Player_PickUp>();
    }


    private void FixedUpdate()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, mask);

        foreach (Collider2D c in col)
        {
            if (c.GetComponent<Item_Component>())
            {
                //here we update to inventory Ui
                Item_Component item = c.transform.gameObject.GetComponent<Item_Component>();
                pickUp.PickedUp(item);

            }
        } 

    }

}
