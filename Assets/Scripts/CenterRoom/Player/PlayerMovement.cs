using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 movInput;
    private Vector2 smoothMovInput;
    private Vector2 movInputSmoothVel;
    private Animator anim;

    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float smoothTime;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        SetPlayerVelocity();
        RotatePlayer();
        SetAnimation();
    }

    private void SetAnimation()
    {
        bool isMoving = movInput != Vector2.zero;
        anim.SetBool("IsMoving", isMoving);
        
    }

    private void SetPlayerVelocity()
    {
        smoothMovInput = Vector2.SmoothDamp(smoothMovInput, movInput, ref movInputSmoothVel, smoothTime);
        rb.velocity = smoothMovInput * speed;
    }

    private void RotatePlayer()
    {
        if(movInput!=Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, smoothMovInput);
            Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation,rotationSpeed*Time.deltaTime);
            rb.MoveRotation(rotation);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        movInput= inputValue.Get<Vector2>();
    }
}
