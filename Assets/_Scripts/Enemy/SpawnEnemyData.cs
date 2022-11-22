using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "StaySober/Spawn Enemy Data")]

public class SpawnEnemyData : ScriptableObject
{
    public int[] SpawnEnemyChanceInPercent;
    public float EnemySpawnSpeed;
}
