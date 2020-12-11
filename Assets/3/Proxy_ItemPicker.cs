using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proxy_ItemPicker : MonoBehaviour
{

    //public

    public LayerMask mask;
    public float pickUpRadius;



    //private

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    private void FixedUpdate()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, pickUpRadius, mask);

        foreach (Collider2D c in col)
        {
            if (c.GetComponent<Item_Component>())
            {
                //here we update to inventory Ui

                Destroy(c.gameObject);
            }
        } 

    }

}
