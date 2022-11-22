using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject _spawnParent;
    [SerializeField] private GameObject _player;

    [Header("EnemyData")]
    [SerializeField] private float _enemySpawnSpeed;
    [SerializeField] private float _enemyTimerCross;
    [SerializeField] private int _enemyLife;
    [SerializeField] private int _enemyAtkDamage;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private float _enemyAtkSpeed;


    private float _spawnCoolDown;
    private Vector2 _areaSize;

    [SerializeField] private GameObject _enemy;

    private void Start()
    {
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
        go.GetComponent<EnemyParent>().EnemyMoving.GetComponent<EnemyManager>().Initialize(_enemyLife, _enemyAtkDamage, _enemySpeed);
        go.GetComponent<EnemyParent>().EnemyCross.GetComponent<SpawnEnemy>().SetSpawnTimer(_enemyTimerCross);
    }



}
