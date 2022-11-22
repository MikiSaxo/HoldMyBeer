using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private GameObject _spawnParent;
    [SerializeField] private GameObject _player;
    [SerializeField] private GameObject _enemy;

    [Header("EnemyData")]
    [SerializeField] private float _enemySpawnSpeed;
    [SerializeField] private float _enemyTimerCross;
    [SerializeField] private float _enemySpeed;
    [SerializeField] private Color _enemyColor;
    [SerializeField] private int _enemyLife;
    [SerializeField] private int _enemyAtkDamage;
    [SerializeField] private float _enemyAtkSpeed;

    [Header("Blanche")]
    [SerializeField] private Color _blancheColor;
    [SerializeField] private int _blancheLife;
    [SerializeField] private int _blancheAtkDamage;
    [SerializeField] private float _blancheAtkSpeed;
    [Header("Rouge")]
    [SerializeField] private Color _rougeColor;
    [SerializeField] private int _rougeLife;
    [SerializeField] private int _rougeAtkDamage;
    [SerializeField] private float _rougeAtkSpeed;
    [Header("Blonde")]
    [SerializeField] private Color _blondeColor;
    [SerializeField] private int _blondeLife;
    [SerializeField] private int _blondeAtkDamage;
    [SerializeField] private float _blondeAtkSpeed;
    [Header("Ambrée")]
    [SerializeField] private Color _ambreeColor;
    [SerializeField] private int _ambreeLife;
    [SerializeField] private int _ambreeAtkDamage;
    [SerializeField] private float _ambreeAtkSpeed;
    [Header("Triple")]
    [SerializeField] private Color _tripleColor;

    [SerializeField] private int _tripleLife;
    [SerializeField] private int _tripleAtkDamage;
    [SerializeField] private float _tripleAtkSpeed;
    [Header("Brune")]
    [SerializeField] private Color _bruneColor;
    [SerializeField] private int _bruneLife;
    [SerializeField] private int _bruneAtkDamage;
    [SerializeField] private float _bruneAtkSpeed;




    private float _spawnCoolDown;
    private Vector2 _areaSize;


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
        go.GetComponent<EnemyParent>().EnemyMoving.GetComponent<EnemyManager>().Initialize(_enemyColor, _enemyLife, _enemyAtkDamage, _enemyAtkSpeed);
        go.GetComponent<EnemyParent>().EnemyCross.GetComponent<SpawnEnemy>().SetSpawnTimer(_enemyTimerCross);
    }



}
