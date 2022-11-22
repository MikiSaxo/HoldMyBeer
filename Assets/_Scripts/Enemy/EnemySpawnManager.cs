using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public static EnemySpawnManager Instance;

    [Header("Setup")]
    [SerializeField] private GameObject _spawnParent;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;

    [Header("EnemyData")]
    [SerializeField] private float _enemySpawnSpeed;
    [SerializeField] private float _enemyTimerCross;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private EnemyData[] _enemyData;

    [SerializeField] private Color _enemyColor;
    [SerializeField] private int _enemyLife;
    [SerializeField] private int _enemyAtkDamage;
    [SerializeField] private float _enemyAtkSpeed;

    private int[] _spawnChance;


    private float _spawnCoolDown;
    private Vector2 _areaSize;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _spawnChance = new int[6];
        UpdateSpawnRate(100, 0, 0, 0, 0, 0);
        _areaSize.x = GetComponent<RectTransform>().rect.width;
        _areaSize.y = GetComponent<RectTransform>().rect.height;
        //print("_spawnCoolDown " + _spawnCoolDown);
    }
    void Update()
    {
        _spawnCoolDown += Time.deltaTime;

        if (_spawnCoolDown > _enemySpawnSpeed)
        {
            _spawnCoolDown -= _enemySpawnSpeed;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var newY = Random.Range(0, _areaSize.y);
        var newX = Random.Range(0, _areaSize.x);
        GameObject go = Instantiate(_enemy, _spawnParent.transform);
        go.transform.position = new Vector2(newX + _spawnParent.transform.position.x, newY + _spawnParent.transform.position.y);

        go.GetComponent<EnemyParent>().EnemyMoving.GetComponent<EnemyMovement>().Initialize(_player, _enemySpeed);
        go.GetComponent<EnemyParent>().EnemyMoving.GetComponent<EnemyManager>().Initialize(_enemyColor, _enemyLife, _enemyAtkDamage, _enemyAtkSpeed);
        go.GetComponent<EnemyParent>().EnemyCross.GetComponent<SpawnEnemy>().SetSpawnTimer(_enemyTimerCross);
    }

    public void UpdateSpawnRate(int blanche, int rouge, int blonde, int ambree, int triple, int brune)
    {
        _spawnChance[0] = blanche;
        _spawnChance[1] = rouge;
        _spawnChance[2] = blonde;
        _spawnChance[3] = ambree;
        _spawnChance[4] = triple;
        _spawnChance[5] = brune;
    }

    private void ChooseRandomBeer()
    {
        int nb = Random.Range(0, 100);
        int smallest = int.MaxValue;


        //float bestDistance = float.MaxValue;
        //EnemyController bestEnemy = null;

        //foreach (var enemy in Enemies)
        //{
        //    Vector3 direction = enemy.transform.position - position;

        //    float distance = direction.sqrMagnitude;

        //    if (distance < bestDistance)
        //    {
        //        bestDistance = distance;
        //        bestEnemy = enemy;
        //    }
        //}

        //for (int i = 0; i < _spawnChance.Length; i++)
        //{
        //    _s
        //}
    }
}
