using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    //public
    public Transform Graphics;
    public Animator HumanAnimator;

    public Sprite[] DirectionsSprites;
    public SpriteRenderer SRenderer;

    //private
    private PlayerMovement _pMovement;

    private Vector2 _directionKeys; // captures input

    enum Direction
    {
        Right,
        Up,
        Down,
        UpR,
        DownR,
    }

    void Start()
    {
        _pMovement = gameObject.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        AnimatorControler();
        _directionKeys = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        _pMovement.ApplyMovement(_directionKeys);
    }

    bool _right;
    bool _up;
    bool _down;
    void AnimatorControler() // change the side you are looking at
    {
        //CheckMovement

        if (_directionKeys != Vector2.zero)
        {
            if (Mathf.Abs(_directionKeys.x) > 0)
                _right = true;
            else
                _right = false;

            if (_directionKeys.y > 0)
                _up = true;
            else
                _up = false;


            if (_directionKeys.y < 0)
                _down = true;
            else
                _down = false;

            HumanAnimator.SetBool("Right", _right);
            HumanAnimator.SetBool("Up", _up);
            HumanAnimator.SetBool("Down", _down);

            Graphics.right = new Vector2(_directionKeys.x, 0);
        }
        else
        {
            //set img Sprite
            HumanAnimator.SetBool("Right", false);
            HumanAnimator.SetBool("Up", false);
            HumanAnimator.SetBool("Down", false);

        }

    }

}
