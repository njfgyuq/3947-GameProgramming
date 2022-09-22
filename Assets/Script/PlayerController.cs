using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2D => GetComponent<Rigidbody2D>();

    [SerializeField] private float jumpForce;
    [SerializeField] private Vector2 groundCheckDimensions;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform feet;

    private bool isGrounded;
    private float horizontalInput;
    private int jumpCount = 0;
    private float mx;
    private float jumpCoolDown;
    

    private void OnJump()
    {
        
        rb2D.velocity += Vector2.up * jumpForce;
    }

    private void OnHorizontalMovement(InputValue axis)
    {
        horizontalInput = axis.Get<float>();
    }

    private void Update()
    {
        CheckForGround();
        if (Input.GetButtonDown("Jump"))
        {
            OnJump();
        }
    }

    private void FixedUpdate()
    {
        rb2D.velocity = new Vector2(horizontalInput * movementSpeed, rb2D.velocity.y);
    }

    private void CheckForGround()
    {
        isGrounded = Physics2D.BoxCast(transform.position,groundCheckDimensions, 0f, 
            -transform.up, 0.1f, platformLayer);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, (Vector3)groundCheckDimensions);
    }
}
