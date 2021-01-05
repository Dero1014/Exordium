using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCollisionItem : MonoBehaviour
{

    private PlayerPickUp _pickUp;

    void Start()
    {
        _pickUp = GameObject.FindObjectOfType<PlayerPickUp>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (this.enabled)
        {
            ItemComponent item = col.GetComponent<ItemComponent>();

            _pickUp.PickedUp(item);
        }
        
    }

}
