using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    //public
    public float Speed;

    //private 
    private Rigidbody2D PlayerRigidBody;

    private Vector2 _directionMovement;

    void Start()
    {
        PlayerRigidBody = gameObject.GetComponent<Rigidbody2D>();
    }


    public void ApplyMovement(Vector2 directionKeys)
    {
        if (directionKeys != Vector2.zero)
        {
            _directionMovement = ((directionKeys.x * transform.right) + (directionKeys.y * transform.up)).normalized; //grab the directional input and normalize it

            _directionMovement = _directionMovement * Speed * Time.deltaTime;  //apply speed to the direction

            PlayerRigidBody.velocity = _directionMovement;  //apply directional movement to the rigidbody of the player

        }
        else
        {
            _directionMovement = Vector2.zero;
            PlayerRigidBody.velocity = _directionMovement;
        }


    }

}
