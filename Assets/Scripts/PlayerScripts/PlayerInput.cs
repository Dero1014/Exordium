using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour
{
    public static PlayerInput current;

    public bool InputOn=true;

    //private
    private PlayerMovement _pMovement;
    private PlayerAnimator _pAnimator;

    private Vector2 _directionKeys; // captures input

    private void Awake()
    {
        current = this;
    }

    public delegate void InputChange();
    public event InputChange OnInputChange;

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

        if (_directionKeys!=Vector2.zero)
        {
            OnInputChange();
        }

        _pAnimator.SetDirection(_directionKeys);
    }

    private void FixedUpdate()
    {
        _pMovement.ApplyMovement(_directionKeys);
    }


}
