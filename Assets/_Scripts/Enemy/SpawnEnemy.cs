using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyMoving;
    private float _spawnTimer;
    private float _spawnCooldown;
    private bool _canMove;
    private bool _hasSpawn;

    private void Awake()
    {
        _enemyMoving.SetActive(false);
    }
    void Start()
    {
        _canMove = true;
        //print("_spawnTimer " + _spawnTimer);
        PlayerManager.Instance.NextLevel += HasNextLevel;
        PlayerManager.Instance.ChooseUpgrade += HasChooseUpgrade;
    }

    private void Update()
    {
        if (_canMove)
            _spawnCooldown += Time.deltaTime;

        if (_spawnCooldown > _spawnTimer && !_hasSpawn)
        {
            _enemyMoving.SetActive(true);
            gameObject.SetActive(false);
            _hasSpawn = true;
        }
    }

    public void SetSpawnTimer(float spawnTimer)
    {
        _spawnTimer = spawnTimer;
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
