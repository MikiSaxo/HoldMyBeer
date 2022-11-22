using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChronoManager : MonoBehaviour
{
    public static ChronoManager Instance;

    [SerializeField] private TextMeshProUGUI _chronoText;
    [SerializeField] private int _timeForNewSpawnRate;
    [SerializeField] private SpawnEnemyData[] _spawnEnemyData;

    private int _countSpawnEnemyData;
    private bool _isGameEnded;
    private float _chrono;
    private float _chronoSpawn;
    private bool _canMove;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        PlayerManager.Instance.NextLevel += HasNextLevel;
        PlayerManager.Instance.ChooseUpgrade += HasChooseUpgrade;
    }

    void Update()
    {
        if (!_isGameEnded && _canMove)
        {
            _chrono += Time.deltaTime;
            _chronoSpawn += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(_chrono);
            _chronoText.text = time.ToString(@"hh\:mm\:ss");
        }
        if (_chronoSpawn >= _timeForNewSpawnRate)
        {
            _chronoSpawn -= _timeForNewSpawnRate;

            if (_countSpawnEnemyData < _spawnEnemyData.Length)
            {
                EnemySpawnManager.Instance.UpdateWhichEnemySpawn(_spawnEnemyData[_countSpawnEnemyData].SpawnEnemyChanceInPercent);
                EnemySpawnManager.Instance.UpdateSpawnRate(_spawnEnemyData[_countSpawnEnemyData].EnemySpawnSpeed);
                _countSpawnEnemyData++;
                EnemySpawnManager.Instance.SpawnEnemy(true);
            }
        }
    }

    public string GetChrono()
    {
        return _chronoText.text;
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
