using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool InputOn=true;

    //private
    private PlayerMovement _pMovement;
    private PlayerAnimator _pAnimator;

    private Vector2 _directionKeys; // captures input


    void Start()
    {
        _pMovement = gameObject.GetComponent<PlayerMovement>();
        _pAnimator = gameObject.GetComponent<PlayerAnimator>();
    }

    void Update()
    {
        if (InputOn)
            _directionKeys = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        else
            _directionKeys = Vector2.zero;

        _pAnimator.SetDirection(_directionKeys);
    }

    private void FixedUpdate()
    {
        _pMovement.ApplyMovement(_directionKeys);
    }


}
