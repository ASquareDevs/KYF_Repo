using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = .1f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float groundDistance = 0.4f;
    
    public CharacterController controller;
    public Transform groundCheck;
    public LayerMask groundMask;

    private float xHorizontalMove;
    private float zVerticalMove;
    private Vector3 move;
    private Vector3 playerVelocity;
    private bool isGrounded;


    private void MoveDirection()
    {
        move = transform.right * xHorizontalMove + transform.forward * zVerticalMove;
        //controller.Move(move * playerSpeed * Time.deltaTime);
        
    } // END MoveDirection


    private void ApplyGravity()
    {
        
        playerVelocity.y += gravity * Time.deltaTime;
        //controller.Move(playerVelocity * Time.deltaTime);
    } // END ApplyGravity


    private void MoveHorizontally()
    {
        xHorizontalMove = Input.GetAxis("Horizontal");
    } // END MoveHorizontally


    private void MoveForward()
    {
        zVerticalMove = Input.GetAxis("Vertical");
    } // END MoveForward


    private void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
    } // END CheckIfGrounded


    void Update()
    {
        CheckIfGrounded();
        MoveHorizontally();
        MoveForward();
        MoveDirection();
        ApplyGravity();
    } // END Update

} // END PlayerMovement
