using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    private PlayerControls controls;
    private Vector2 move;
    private Rigidbody rb;
    private Vector2 walk;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls();

        // Bind the Move action
        controls.PlayerControlz.Moving.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.PlayerControlz.Moving.canceled += ctx => move = Vector2.zero;

        // Bind the Walk action for 1D axis movement

        controls.PlayerControlz.Walking.performed += ctx => walk = ctx.ReadValue<Vector2>();
        controls.PlayerControlz.Walking.canceled += ctx => walk = Vector2.zero;
        

    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
     
        Vector3 movement = (walk != Vector2.zero) ? new Vector3(walk.x, 0, walk.y) * moveSpeed * Time.fixedDeltaTime : new Vector3(move.x, 0, move.y) * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }
}

