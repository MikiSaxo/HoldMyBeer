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
    [SerializeField] private float bulletShoot;

    Vector2 movementInput = Vector2.zero;
    private float nextShoot;

    private void Start()
    {
    }

    void Update()
    {
        if (movementInput.sqrMagnitude > 0)
            Movement();

        nextShoot += Time.deltaTime;

        if (nextShoot > bulletShoot)
        {
            nextShoot -= bulletShoot;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

 
    void Movement()
    {
        Vector2 move = new Vector2(movementInput.x, movementInput.y) * Speed;
        rb.velocity = move;
    }
}