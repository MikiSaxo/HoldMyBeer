using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _enemyMoving;
    private float _spawnTimer;

    private void Awake()
    {
        _enemyMoving.SetActive(false);
    }
    void Start()
    {
        //print("_spawnTimer " + _spawnTimer);
        StartCoroutine(SpawnTimerEnemy());
    }

    IEnumerator SpawnTimerEnemy()
    {
        yield return new WaitForSeconds(_spawnTimer);
        _enemyMoving.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetSpawnTimer(float spawnTimer)
    {
        _spawnTimer = spawnTimer;
    }
}
