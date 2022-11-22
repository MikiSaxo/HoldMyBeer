using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private int _life;
    [SerializeField] private float _playerSpeed;
    [SerializeField] private SpriteRenderer _playerSprite;
    private int _xp;
    [SerializeField] private int _xpToReach;
    [SerializeField] private int _xpToIncreaseEachStep;

    // [Header("Crit")]
    // [Tooltip("In %")] [SerializeField] private float _critChance;
    // [SerializeField] private int _critDamage;

    [Header("Ranged")]
    [Tooltip("Per sec")] [SerializeField] private float _rangedAtkSpeed;
    [SerializeField] private int _rangedAtkDamage;
    [SerializeField] private float _rangedBulletSpeed;

    [Header("UI")]
    [SerializeField] private GameObject[] _cardsNextLevelMenu;
    [SerializeField] private Image _lifeBar;
    [SerializeField] private Image _xpBar;
    [SerializeField] private GameObject _nextLevelMenu;
    [SerializeField] private GameObject _endGameMenu;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private TextMeshProUGUI _endChrono;

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
        if (!_canMove) return;

        _life += value;
        if (_life > 100)
            _life = 100;
        else if(_life <= 0)
            PlayerIsDead();

        if (value < 0)
            StartCoroutine(TakeDamage());

        _lifeBar.fillAmount = (float)_life / 100f;
    }

    IEnumerator TakeDamage()
    {
        _playerSprite.color = Color.red;
        yield return new WaitForSeconds(.1f);
        _playerSprite.color = Color.white;
    }

    private void PlayerIsDead()
    {
        NextLevel?.Invoke();
        _endGameMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(_restartButton);
        var endChrono =ChronoManager.Instance.GetChrono();
        _endChrono.text = "Your time : " + endChrono;
    }

    public void RestartGame()
    {
        ChooseUpgrade?.Invoke();
        SceneManager.LoadScene(0);
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
        _xpBar.fillAmount = (float)_xp / (float)_xpToReach;
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
