using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    //public
    public Transform Graphics;
    public Animator HumanAnimator;

    //private

    private Vector2 _directionKeys; // captures input
    private int _idUp;
    private int _idDown;
    private int _idRight;
    void Start()
    {
        HumanAnimator = GetComponentInChildren<Animator>();

        _idUp = Animator.StringToHash("Up");
        _idDown = Animator.StringToHash("Down");
        _idRight = Animator.StringToHash("Right");
    }

    void Update()
    {
        AnimatorControler();
    }

    public void SetDirection(Vector2 dir)
    {
        _directionKeys = dir;
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

            HumanAnimator.SetBool(_idRight, _right);
            HumanAnimator.SetBool(_idUp, _up);
            HumanAnimator.SetBool(_idDown, _down);

            Graphics.right = new Vector2(_directionKeys.x, 0);
        }
        else
        {
            HumanAnimator.SetBool(_idRight, false);
            HumanAnimator.SetBool(_idUp, false);
            HumanAnimator.SetBool(_idDown, false);
        }

    }
}
