using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    //public
    public Transform graphics;

    //private
    private Player_Movement pMovement;


    private Vector2 directionKeys; // captures input

    void Start()
    {
        pMovement = gameObject.GetComponent<Player_Movement>();
    }

    void Update()
    {
        SwitchSides();
        directionKeys = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        pMovement.ApplyMovement(directionKeys);
    }

    void SwitchSides() // change the side you are looking at
    {
        if (directionKeys != Vector2.zero)
        {
            graphics.right = new Vector2(directionKeys.x, 0);
        }
    }
}
