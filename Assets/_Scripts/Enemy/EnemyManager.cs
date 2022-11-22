using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject _xpObj;

    private Color _color;
    private int _life;
    private int _damage;
    private float _atkSpeed;
    private float _atkCooldown;
    private bool _hasAtk;

    public void Initialize(Color color, int life, int damage, float atkSpeed)
    {
        _color = color;
        _life = life;
        _damage = damage;
        _atkSpeed = atkSpeed;
    }

    public void UpdateLife(int value)
    {
        _life += value;

        if(_life <= 0)
        {
            Instantiate(_xpObj, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        _atkCooldown += Time.deltaTime;

        //if (_atkCooldown > _atkSpeed && _hasAtk)
        //{
        //    _atkCooldown -= _atkSpeed;
        //}
    }

    private void AtkPlayer()
    {
        PlayerManager.Instance.UpdateLife(-_damage);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<BulletMovement>())
        {
            //print("une bullet m'a touchée " + PlayerManager.Instance.GetRangedDamage() + " life " + _life);
            UpdateLife(-PlayerManager.Instance.GetRangedDamage());
            Destroy(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerManager>())
        {
            if(_atkCooldown > _atkSpeed)
            {
                _atkCooldown -= _atkSpeed;
                AtkPlayer();
            }
        }
    }
}
