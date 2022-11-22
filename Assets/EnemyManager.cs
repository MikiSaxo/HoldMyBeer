using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _life;
    private int _damage;
    private float _atkSpeed;
    private float _atkCooldown;

    public void Initialize(int life, int damage, float atkSpeed)
    {
        _life = life;
        _damage = damage;
        _atkSpeed = atkSpeed;
    }

    public void UpdateLife(int value)
    {
        _life += value;

        if(_life <= 0)
        {
            //Spawn XP
            Destroy(gameObject);
        }
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
}
