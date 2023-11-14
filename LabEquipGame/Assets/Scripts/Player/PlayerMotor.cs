using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// FMOD stuff
using FMOD;
using FMODUnity;
using FMOD.Studio;

public class PlayerMotor : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool isGrounded;
    public float speed = 5f;
    public float gravity = -9.8f;
    public float jumpHeight = 3f;
    private bool isKnockedBack = false;
    private float knockbackDuration = 0.25f; // Adjust the duration as needed
    private float knockbackTimer = 0f;

    public FMODUnity.EventReference jump; //FMOD event reference for jump sound
    FMOD.Studio.EventInstance jumpSFX; // The instance of the event


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        if (isKnockedBack)
        {
            knockbackTimer -= Time.deltaTime;

            if (knockbackTimer <= 0f)
            {
                isKnockedBack = false;
                playerVelocity = Vector3.zero; // Reset velocity after knockback duration
            }
        }

    }
    //take inpputs from InputManager, apply to character controller
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        playerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && playerVelocity.y < 0)
            playerVelocity.y = -2f;
        controller.Move(playerVelocity * Time.deltaTime);
        
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);

            //Jump sound
            jumpSFX = FMODUnity.RuntimeManager.CreateInstance(jump); //creates the event
            jumpSFX.start();    //plays the event
            jumpSFX.release(); // destroys the event after it plays

        }
    }

    public void Knockback()
    {
        if (!isKnockedBack)
        {
            playerVelocity = -transform.forward * 22f;
            isKnockedBack = true;
            knockbackTimer = knockbackDuration;
        }
        
    }
}
