using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Hp = 100
    //Movement speed = 5(améliorable, +0.1 par niveau)
    //Crit rate = 20 % (améliorable, +10 % par niveau, max 100)
    //Crit damage = 50 % (+5 % par niveau, infini)

    //Attack speed = 1(améliorable, +0.25 par niveau), infini
    //Attack damage = 5(améliorable, +3 par niveau)
    //Attack angle = 60(améliorable, +20° par niveau, non améliorable si angle = 360°)
    //Attack range = 1(améliorable, +0.15 par niveau), max 2

    //Attack speed = 0.5(améliorable, +0.3 par niveau, infini)
    //Attack damage = 2(améliorable, +2 * par niveau, infini)

    [Header("Setup")]
    [SerializeField] private int _life;
    [SerializeField] private float _playerSpeed;

    [Header("Crit")]
    [Tooltip("In %")] [SerializeField] private float _critChance; 
    [SerializeField] private int _critDamage;

    [Header("Melee")]
    [Tooltip("Per sec")] [SerializeField] private float _meleeAtkSpeed;
    [SerializeField] private float _meleeAtkDamage;
    [SerializeField] private float _meleeAtkAngle;
    [Tooltip("1 is the original range")] [SerializeField] private float _meleeAtkRange;

    [Header("Ranged")]
    [Tooltip("Per sec")] [SerializeField] private float _rangedAtkSpeed;
    [SerializeField] private float _rangedAtkDamage;
    [SerializeField] private float _rangedBulletSpeed;


    public static PlayerManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetRanged();
        SetMelee();
        SetSpeedPlayer();
    }

    public float GetRangedDamage()
    {
        return _rangedAtkDamage;
    }
    public float GetMeleeDamage()
    {
        return _meleeAtkDamage;
    }

    public void SetRanged()
    {
        PlayerAim.Instance.UpdateAimValues(_rangedBulletSpeed, _rangedAtkSpeed, _meleeAtkSpeed, _meleeAtkAngle, _meleeAtkRange);
    }

    public void SetMelee()
    {

    }

    public void SetSpeedPlayer()
    {
        PlayerMovement.Instance.UpdateSpeedValue(_playerSpeed);
    }
}
