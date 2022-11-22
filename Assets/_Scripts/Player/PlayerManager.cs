using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private int _xp;
    [SerializeField] private int _xpToReach;
    [SerializeField] private int _xpToIncreaseEachStep;

    [Header("Crit")]
    [Tooltip("In %")] [SerializeField] private float _critChance; 
    [SerializeField] private int _critDamage;

    [Header("Melee")]
    [Tooltip("Per sec")] [SerializeField] private float _meleeAtkSpeed;
    [SerializeField] private int _meleeAtkDamage;
    [SerializeField] private float _meleeAtkAngle;
    [Tooltip("1 is the original range")] [SerializeField] private float _meleeAtkRange;

    [Header("Ranged")]
    [Tooltip("Per sec")] [SerializeField] private float _rangedAtkSpeed;
    [SerializeField] private int _rangedAtkDamage;
    [SerializeField] private float _rangedBulletSpeed;

    [Header("UI")]
    [SerializeField] private Image _lifeBar;
    [SerializeField] private Image _xpBar;

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

    public int GetRangedDamage()
    {
        return _rangedAtkDamage;
    }
    public int GetMeleeDamage()
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

    public void UpdateLife(int value)
    {
        //print("life " + _life);
        _life += value;
        _lifeBar.fillAmount = (float)_life / 100f;
    }
    
    public void UpdateXP(int value)
    {
        _xp += value;

        if(_xp >= _xpToReach)
        {
            _xp = 0;
            _xpToReach += _xpToIncreaseEachStep;
            //LauchMenu Upgrades
        }
        //print("hello xp " + (float)_xp / (float)_xpToReach);
        _xpBar.fillAmount = (float)_xp / (float)_xpToReach;// / 100;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<XpObject>())
        {
            UpdateXP(1);
            Destroy(collision.gameObject);
        }
    }
}
