using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    /*[SerializeField] private Rigidbody2D rb;
    [SerializeField] private float jumpForce = 5f;



    private void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
           rb.AddForce((jumpForce * transform.up), ForceMode2D.Impulse); 
        }
    }*/

    /*public float moveSpeed = 1f;

    private Vector2 moveInput;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }*/
    
    Rigidbody2D rb2D => GetComponent<Rigidbody2D>();

    [SerializeField] private float jumpForce;
    [SerializeField] private Vector2 groundCheckDimensions;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private float movementSpeed;

    private bool isGrounded;
    private float horizontalInput;
    

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
