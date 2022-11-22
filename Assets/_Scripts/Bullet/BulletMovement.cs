using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private float _speed;
    private bool _canMove;
    private Vector3 _direction;
    [SerializeField] private float _timeToDie;
    private float _timeToDieCooldown;

    public void Initialize(Vector3 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
    }

    void Start()
    {
        _canMove = true;
        PlayerManager.Instance.NextLevel += HasNextLevel;
        PlayerManager.Instance.ChooseUpgrade += HasChooseUpgrade;
    }

    void Update()
    {
        if (_canMove)
        {
            Move();
            _timeToDieCooldown += Time.deltaTime;
        }

        if (_timeToDieCooldown > _timeToDie)
        {
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position += _direction * _speed * Time.deltaTime;
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
