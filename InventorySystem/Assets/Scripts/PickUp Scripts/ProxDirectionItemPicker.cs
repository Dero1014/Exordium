using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProxDirectionItemPicker : MonoBehaviour
{

    //public
    public LayerMask Mask;
    public float PickUpRadius;
    public float Distance;
    [HideInInspector] public Vector2 PosInDirection;

    //private
    private Vector2 _directionInput;

    private PlayerPickUp _pickUp;

    void Start()
    {
        _pickUp = GameObject.FindObjectOfType<PlayerPickUp>();
    }

    void Update()
    {
        _directionInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    }

    private void FixedUpdate()
    {
        PosInDirection = ((Vector2)transform.position) + (_directionInput * Distance) ;
        if (_directionInput!=Vector2.zero)
        {
            Collider2D[] col = Physics2D.OverlapCircleAll(PosInDirection, PickUpRadius, Mask);

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
}
