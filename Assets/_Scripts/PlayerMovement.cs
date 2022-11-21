using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float Speed;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] SpriteRenderer MainSprite;
    [SerializeField] private float dashRate;

    Vector2 movementInput = Vector2.zero;
    private float nextDash;

    private void Start()
    {
    }

    void Update()
    {
        if (movementInput != Vector2.zero)
            Movement();

        nextDash += Time.deltaTime;

        if (nextDash > dashRate)
        {
            nextDash -= dashRate;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

 
    void Movement()
    {
        Vector2 m2 = new Vector2(movementInput.x, movementInput.y) * Speed;
        rb.velocity = m2;
    }
}