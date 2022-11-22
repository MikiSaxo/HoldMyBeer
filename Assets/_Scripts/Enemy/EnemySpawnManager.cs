using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("Setup")]
    [SerializeField] private GameObject _spawnParent;
    [SerializeField] private GameObject _player;
    private GameObject _goodEnemy;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _oliver;

    [Header("EnemyData")]
    [SerializeField] private float _enemyTimerCross;
    [SerializeField] private float _enemySpeedMovement;
    [SerializeField] private EnemyData[] _enemyData;
    [SerializeField] private SpawnEnemyData _firstSpawnEnemyData;

    private Color _enemyColor;
    private int _enemyLife;
    private int _enemyAtkDamage;
    private float _enemyAtkSpeed;
    private float _enemySpawnSpeed;

    private int[] _spawnChance;


    private float _spawnCoolDown;
    private Vector2 _areaSize;
    private bool _canMove;

    private void Awake()
    {
        Instance = this;
    }

    const int nbOfBeer = 6;
    private void Start()
    {
        _spawnChance = new int[nbOfBeer];

        UpdateWhichEnemySpawn(_firstSpawnEnemyData.SpawnEnemyChanceInPercent);
        UpdateSpawnRate(_firstSpawnEnemyData.EnemySpawnSpeed);

        _areaSize.x = GetComponent<RectTransform>().rect.width;
        _areaSize.y = GetComponent<RectTransform>().rect.height;
        PlayerManager.Instance.NextLevel += HasNextLevel;
        PlayerManager.Instance.ChooseUpgrade += HasChooseUpgrade;
    }
    void Update()
    {
        if (_canMove)
            _spawnCoolDown += Time.deltaTime;

        if (_spawnCoolDown > _enemySpawnSpeed)
        {
            _spawnCoolDown -= _enemySpawnSpeed;
            SpawnEnemy(false);
        }
    }

    private const int OLIVER_EXTRA_LIFE_POWER = 6;
    public void SpawnEnemy(bool isOliver)
    {
        ChooseRandomBeer();
        var newY = Random.Range(0, _areaSize.y);
        var newX = Random.Range(0, _areaSize.x);


        _goodEnemy = isOliver ? _oliver : _enemy;

        GameObject go = Instantiate(_goodEnemy, _spawnParent.transform);
        go.transform.position = new Vector2(newX + _spawnParent.transform.position.x, newY + _spawnParent.transform.position.y);

        go.GetComponent<EnemyParent>().EnemyMoving.GetComponent<EnemyMovement>().Initialize(_player, _enemySpeedMovement);

        if (!isOliver)
        {
            go.GetComponent<EnemyParent>().EnemyCross.GetComponent<SpawnEnemy>().SetSpawnTimer(_enemyTimerCross);
            go.GetComponent<EnemyParent>().EnemyMoving.GetComponent<EnemyManager>().Initialize(_enemyColor, _enemyLife, _enemyAtkDamage, _enemyAtkSpeed);
        }
        else
            go.GetComponent<EnemyParent>().EnemyMoving.GetComponent<EnemyManager>().Initialize(_enemyColor, _enemyLife * OLIVER_EXTRA_LIFE_POWER, _enemyAtkDamage, _enemyAtkSpeed * 2);
    }


    public void UpdateWhichEnemySpawn(int[] wave)
    {
        for (int i = 0; i < _spawnChance.Length; i++)
        {
            _spawnChance[i] = wave[i];
        }
    }

    public void UpdateSpawnRate(float enemySpawnSpeed)
    {
        _enemySpawnSpeed = enemySpawnSpeed;
    }

    private void ChooseRandomBeer()
    {
        int nb = Random.Range(0, 100);
        int smallest = int.MaxValue;


        for (int i = 0; i < _spawnChance.Length; i++)
        {
            if (_spawnChance[i] == 0 || _spawnChance[i] < nb)
                continue;

            var number = Mathf.Abs(_spawnChance[i] - nb);

            if (number < smallest)
            {
                smallest = number;
                _enemyColor = _enemyData[i].Color;
                _enemyLife = _enemyData[i].Life;
                _enemyAtkDamage = _enemyData[i].AtkDamage;
                _enemyAtkSpeed = _enemyData[i].AtkSpeed;
            }
        }
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
