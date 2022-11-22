using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float _speed;

    private Vector3 _direction;

    public void Initialize(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    void Start()
    {
        Destroy(gameObject, 5);
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
