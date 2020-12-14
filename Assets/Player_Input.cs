using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Input : MonoBehaviour
{
    //public
    public Transform graphics;
    public Animator humanAnimator;

    public Sprite[] directionsSprites;
    public SpriteRenderer sRenderer;

    //private
    private Player_Movement pMovement;

    private Vector2 directionKeys; // captures input

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
        pMovement = gameObject.GetComponent<Player_Movement>();
    }

    void Update()
    {
        AnimatorControler();
        directionKeys = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void FixedUpdate()
    {
        pMovement.ApplyMovement(directionKeys);
    }

    bool right;
    bool up;
    bool down;
    void AnimatorControler() // change the side you are looking at
    {
        //CheckMovement

        if (directionKeys != Vector2.zero)
        {
            if (Mathf.Abs(directionKeys.x) > 0)
                right = true;
            else
                right = false;

            if (directionKeys.y > 0)
                up = true;
            else
                up = false;


            if (directionKeys.y < 0)
                down = true;
            else
                down = false;

            humanAnimator.SetBool("Right", right);
            humanAnimator.SetBool("Up", up);
            humanAnimator.SetBool("Down", down);

            graphics.right = new Vector2(directionKeys.x, 0);
        }
        else
        {
            //set img Sprite

            humanAnimator.SetBool("Right", false);
            humanAnimator.SetBool("Up", false);
            humanAnimator.SetBool("Down", false);

        }



    }
}
