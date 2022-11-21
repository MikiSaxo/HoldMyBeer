using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public static PlayerAim Instance;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private GameObject _sprite;
    [SerializeField] private float _shootRate;

    private Vector2 _aimInput;
    private Vector3 _transferPosition;
    private float _shootCooldown;


    private void Awake()
    {
        Instance = this;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _aimInput = context.ReadValue<Vector2>();
    }


    void Update()
    {
        if (_aimInput.sqrMagnitude > 0)
            Aim();
        
        _shootCooldown += Time.deltaTime;

        if (_shootCooldown > _shootRate)
        {
            _shootCooldown -= _shootRate;
        }

    }

    void Aim()
    {
        if (_aimInput != Vector2.zero)
        {
            float angle = Mathf.Atan2(_aimInput.y, _aimInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }

        _transferPosition = new Vector3(_aimInput.x + transform.position.x, _aimInput.y + transform.position.y, 0);
    }

    void Shoot()
    {
       
    }
}