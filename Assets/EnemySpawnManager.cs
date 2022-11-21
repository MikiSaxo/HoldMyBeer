using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private float _spawnSpeed;
    [SerializeField] private GameObject _spawnParent;
    private float _spawnCoolDown;
    private Vector2 _areaSize;

    [SerializeField] private GameObject _enemy;

    private void Start()
    {
        _areaSize.x = GetComponent<RectTransform>().rect.width;
        _areaSize.y = GetComponent<RectTransform>().rect.height;
    }
    void Update()
    {
        _spawnCoolDown += Time.deltaTime;

        if (_spawnCoolDown > _spawnSpeed)
        {
            _spawnCoolDown -= _spawnSpeed;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        var Y = Random.Range(0, _areaSize.y);
        var X = Random.Range(0, _areaSize.x);
        GameObject go = Instantiate(_enemy, _spawnParent.transform);
        go.transform.position = new Vector2(X + _spawnParent.transform.position.x, Y + _spawnParent.transform.position.y);
    }
}
