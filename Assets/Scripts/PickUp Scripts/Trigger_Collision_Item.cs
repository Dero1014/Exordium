using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger_Collision_Item : MonoBehaviour
{

    private Player_PickUp pickUp;

    void Start()
    {
        pickUp = GameObject.FindObjectOfType<Player_PickUp>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (this.enabled)
        {
            Item_Component item = col.GetComponent<Item_Component>();

            pickUp.PickedUp(item);
        }
        


    }

   

}
