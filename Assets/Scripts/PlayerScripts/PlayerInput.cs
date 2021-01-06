using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    //private
    private PlayerMovement _pMovement;

    private Vector2 _directionKeys; // captures input


    void Start()
    {
        _pMovement = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        _directionKeys = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        _pMovement.ApplyMovement(_directionKeys);
    }

   

}
