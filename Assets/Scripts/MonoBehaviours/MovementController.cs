﻿using UnityEngine;

public class MovementController : MonoBehaviour
{
    // public variables appear as properties in Unity's inspector window
    public float movementSpeed = 3.0f;

    // holds 2D points; used to represent a character's location in 2D space, or where it's moving to
    Vector2 movement = new Vector2();

    // holds reference to the animator component in the game object
    Animator animator;

    // reference to the character's Rigidbody2D component
    Rigidbody2D rb2D;

    // use this for initialization
    private void Start()
    {
        // get references to game object components so they don't have to be grabbed each time they are needed
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // called once per frame
    private void Update()
    {
        // update the animation state machine
        UpdateState();
    }

    // called at fixed intervals by the Unity engine
    // update may be called less frequently on slower hardware when frame rate slows down
    void FixedUpdate()
    {
        MoveCharacter();
    }

    private void UpdateState()
    {
        // Check to see if the movement vector is approximately equal to (0, 0) -- i.e. player is standing still
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }

        // Update the animator with the new movement values
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }

    private void MoveCharacter()
    {
        // get user input
        // GetAxisRaw parameter allows us to specify which axis we're interested in
        // Returns 1 = right key or "d" (up key or "w")
        //        -1 = left key or "a"  (down key or "s")
        //         0 = no key pressed
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // keeps player moving at the same rate of speed, no matter which direction they are moving in
        movement.Normalize();

        // set velocity of RigidBody2D and move it
        rb2D.velocity = movement * movementSpeed;
    }
}
