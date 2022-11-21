using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _mainSprite;
    

    Vector2 movementInput = Vector2.zero;

    private void Start()
    {
    }

    void Update()
    {
        if (movementInput.sqrMagnitude > 0)
            Movement();
        else
            _rb.velocity = Vector2.zero;

        
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        movementInput = context.ReadValue<Vector2>();
    }

 
    void Movement()
    {
        Vector2 move = new Vector2(movementInput.x, movementInput.y) * _speed;
        _rb.velocity = move;
    }
}