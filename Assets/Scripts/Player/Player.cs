using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] float moveSpeed = 10f;
    private Animator animator;
    private Vector2 movement;
    Vector3 forward, right;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        InitializeMovement();
    }

    void FixedUpdate()
    {
        Movement();
    }

    void InitializeMovement()
    {
        forward = UnityEngine.Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    void Movement()
    {
        if (movement.x != 0 || movement.y != 0) {
            animator.SetTrigger("Moving");
        } else {
            animator.ResetTrigger("Moving");
        }

        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * movement.x;
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * movement.y;

        controller.Move(rightMovement);
        controller.Move(upMovement);
        
        if(rightMovement != Vector3.zero) {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), 0.2f);
        }
    }

    public void GetMovementInput(InputAction.CallbackContext value)
    {
        Vector2 _direction = value.ReadValue<Vector2>();
        movement = new Vector2(_direction.x, _direction.y);
    }
}