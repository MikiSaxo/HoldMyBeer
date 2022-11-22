using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance;

    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private SpriteRenderer _spriteNormal;

    //[SerializeField] private SpriteRenderer _mainSprite;

    private bool _canMove;
    private float _speed;
    private Vector2 _movementInput;


    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlayerManager.Instance.NextLevel += HasNextLevel;
        PlayerManager.Instance.ChooseUpgrade += HasChooseUpgrade;
    }

    void Update()
    {
        if (_movementInput.sqrMagnitude > 0 && _canMove)
            Movement();
        else
            _rb.velocity = Vector2.zero;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        _movementInput = context.ReadValue<Vector2>();
        _movementInput.Normalize();
    }

 
    void Movement()
    {
        Vector2 move = new Vector2(_movementInput.x, _movementInput.y) * _speed;
        _rb.velocity = move;

        _spriteNormal.flipX = _rb.velocity.x > 0 ? true : false;
    }

    public void UpdateSpeedValue(float speed)
    {
        _speed = speed;
    }

    private void HasNextLevel()
    {
        _canMove = false;
    }

    private void HasChooseUpgrade()
    {
        _canMove = true;
    }

    private void OnDisable()
    {
        PlayerManager.Instance.NextLevel -= HasNextLevel;
        PlayerManager.Instance.ChooseUpgrade -= HasChooseUpgrade;
    }
}