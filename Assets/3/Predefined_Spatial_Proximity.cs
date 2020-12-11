using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predefined_Spatial_Proximity : MonoBehaviour
{
    //public
    public LayerMask mask;
    public float maxDistance;

    //private
    private Vector2 mousePosition;
    private Vector2 distanceFromClick;

    void Update()
    {

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        

        if (Input.GetMouseButtonDown(0))
        {
            GetItem();
        }
       

    }

    void GetItem()
    {

        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.up, 1, mask);

        if (hit.collider != null)
        {
            print("Hit Something");
            if (hit.transform.GetComponent<Item_Component>())
            {
                distanceFromClick = (Vector2)transform.position - (Vector2)hit.transform.position;
                
                
                if (distanceFromClick.magnitude < maxDistance)
                {
                    //here we update to inventory Ui

                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }


}
