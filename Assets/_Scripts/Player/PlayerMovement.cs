using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField] private Rigidbody2D _rb;
    //[SerializeField] private SpriteRenderer _mainSprite;
    
    private float _speed;
    private Vector2 movementInput;


    private void Awake()
    {
        Instance = this;
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
        movementInput.Normalize();
    }

 
    void Movement()
    {
        print("oui");
        Vector2 move = new Vector2(movementInput.x, movementInput.y) * _speed;
        _rb.velocity = move;
    }

    public void UpdateSpeedValue(float speed)
    {
        _speed = speed;
    }
}