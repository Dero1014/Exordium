using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    //public
    public float speed;

    //private 
    private Rigidbody2D pRigidBody;
    private Animator pAnimator;

    private Vector2 directionMovement;

    void Start()
    {
        pRigidBody = gameObject.GetComponent<Rigidbody2D>();
        pAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        AnimationControl(); //check animation
    }

    public void ApplyMovement(Vector2 directionKeys)
    {
        if (directionKeys != Vector2.zero)
        {
            directionMovement = ((directionKeys.x * transform.right) + (directionKeys.y * transform.up)).normalized; //grab the directional input and normalize it

            directionMovement = directionMovement * speed * Time.deltaTime;  //apply speed to the direction

            pRigidBody.velocity = directionMovement;  //apply directional movement to the rigidbody of the player

        }
        else
        {
            directionMovement = Vector2.zero;
            pRigidBody.velocity = directionMovement;
        }


    }

    void AnimationControl() //controls the flow of animation
    {
        pAnimator.SetFloat("Speed", directionMovement.magnitude);
    }
}
