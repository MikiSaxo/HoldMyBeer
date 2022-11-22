using System;
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

    [Header("Ranged")]
    [Tooltip("Per sec")] [SerializeField] private float _rangedAtkSpeed;
    [SerializeField] private int _rangedAtkDamage;
    [SerializeField] private float _rangedBulletSpeed;

    [Header("UI")]
    [SerializeField] private Image _lifeBar;
    [SerializeField] private Image _xpBar;
    [SerializeField] private GameObject _nextLevelMenu;
    [SerializeField] private GameObject[] _cardsNextLevelMenu;

    [Header("Life")]
    [SerializeField] private float _timeToRegen;
    [SerializeField] private int _regen;
    private float _regenCooldown;

    [Header("Upgrades Datas")]
    [SerializeField] private UpgradesData[] _upgradesData;

    private bool _canMove;

    public static PlayerManager Instance;
    public event Action NextLevel;
    public event Action ChooseUpgrade;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SetRanged();
        SetSpeedPlayer();
        ChooseUpgrade?.Invoke();
        _canMove = true;
        PlayerAim.Instance.UpdateBulletSpeed(_rangedBulletSpeed);
    }

    private void Update()
    {
        if(_canMove)
            _regenCooldown += Time.deltaTime;
        if(_regenCooldown > _timeToRegen )
        {
            _regenCooldown -= _timeToRegen;
            UpdateLife(_regen);
        }
    }

    public int GetRangedDamage()
    {
        return _rangedAtkDamage;
    }

    public void SetRanged()
    {
        PlayerAim.Instance.UpdateAimSpeed(_rangedAtkSpeed);
    }

    public void SetSpeedPlayer()
    {
        PlayerMovement.Instance.UpdateSpeedValue(_playerSpeed);
    }

    public void UpdateLife(int value)
    {
        //print("life " + _life);
        if (!_canMove) return;

        _life += value;
        if (_life > 100)
            _life = 100;

        _lifeBar.fillAmount = (float)_life / 100f;
    }

    public void UpdateXP(int value)
    {
        _xp += value;

        if (_xp >= _xpToReach)
        {
            _xp = 0;
            _xpToReach += _xpToIncreaseEachStep;
            HasPassedALevel();
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

    public void TestNExtLevelButton()
    {
        HasPassedALevel();
    }

    private void HasPassedALevel()
    {
        NextLevel?.Invoke();
        _canMove = false;
        _nextLevelMenu.SetActive(true);

        for (int i = 0; i < _cardsNextLevelMenu.Length; i++)
        {
            _cardsNextLevelMenu[i].GetComponent<CardUpgrade>().Initialize(_upgradesData[i]);
        }
    }

    public void GetNewUpgrades(int which)
    {
        UpgradesData upData = _upgradesData[which];

        if (_rangedAtkSpeed <= 0.2f && which == 1)
            return;

        _playerSpeed += upData.MovementSpeed;
        SetSpeedPlayer();
        
        if (_rangedAtkSpeed >= 0.2f)
        {
            _rangedAtkSpeed += upData.AtkSpeed;
            SetRanged();
        }

        _rangedAtkDamage += upData.AtkDamage;

        ChooseUpgrade?.Invoke();
        _canMove = true;
        _nextLevelMenu.SetActive(false);
    }
}
