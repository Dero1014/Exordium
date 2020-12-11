using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prox_Direction_ItemPicker : MonoBehaviour
{

    //public

    public LayerMask mask;
    public float pickUpRadius;
    public float distance;

    //private
    [HideInInspector] public Vector2 posInDirection;

    private Vector2 directionInput;

    void Update()
    {

        directionInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

    }

    private void FixedUpdate()
    {
        posInDirection = ((Vector2)transform.position) + (directionInput * distance) ;
        if (directionInput!=Vector2.zero)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(posInDirection, pickUpRadius, mask);

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
}
