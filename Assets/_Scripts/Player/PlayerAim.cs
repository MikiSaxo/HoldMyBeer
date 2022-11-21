using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAim : MonoBehaviour
{
    public static PlayerAim Instance;

    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private GameObject _sprite;
    private float _shootRate;
    private float _bulletSpeed;

    private Vector2 _aimInput;
    private Vector2 _aimInputLast;
    private Vector3 _transferPosition;
    private float _shootCooldown;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _aimInputLast = new Vector2(1, 0);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        _aimInput = context.ReadValue<Vector2>();
        _aimInput.Normalize();
    }


    void Update()
    {
        _transferPosition = new Vector3(_aimInputLast.x + transform.position.x, _aimInputLast.y + transform.position.y, 0);

        if (_aimInput.sqrMagnitude > 0)
            Aim();

        _shootCooldown += Time.deltaTime;

        if (_shootCooldown > _shootRate)
        {
            _shootCooldown -= _shootRate;
            Shoot();
        }

    }

    void Aim()
    {
        _aimInputLast = _aimInput;
        
        float angle = Mathf.Atan2(_aimInputLast.y, _aimInputLast.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        var rotation = transform.rotation;
        rotation *= Quaternion.Euler(0, 0, 90);
        GameObject bullet = Instantiate(_bullet, _transferPosition, rotation);
        bullet.transform.SetParent(_bulletParent);
        bullet.GetComponent<BulletMovement>().Initialize(_aimInputLast, _bulletSpeed);
    }

    public void UpdateAimValues(float bulletSpd, float shootRate)
    {
        _bulletSpeed = bulletSpd;
        _shootRate = shootRate;
    }
}