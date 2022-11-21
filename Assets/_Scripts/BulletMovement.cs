using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 10;

    private Vector3 _direction;

    public void Initialize(Vector3 direction)
    {
        _direction = direction;
    }

    void Start()
    {
        Destroy(gameObject, 10);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += _direction * _speed * Time.deltaTime;
    }
}
