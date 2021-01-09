using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PredefinedSpatialProximity : MonoBehaviour
{
    //public
    public LayerMask Mask;
    public float MaxDistance;

    //private
    private Vector2 _mousePosition;
    private Vector2 _distanceFromClick;

    private PlayerPickUp _pickUp;

    void Start()
    {
        _pickUp = GameObject.FindObjectOfType<PlayerPickUp>();
    }


    void Update()
    {
        _mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
            GetItem();

    }

    void GetItem()
    {

        RaycastHit2D hit = Physics2D.Raycast(_mousePosition, Vector2.up, 1, Mask);

        if (hit.collider != null)
        {
            if (hit.transform.GetComponent<ItemComponent>())
            {
                _distanceFromClick = (Vector2)transform.position - (Vector2)hit.transform.position;
                
                if (_distanceFromClick.magnitude < MaxDistance)
                {
                    ItemComponent item = hit.transform.gameObject.GetComponent<ItemComponent>();
                    _pickUp.PickedUp(item);

                }
            }
        }
    }


}
