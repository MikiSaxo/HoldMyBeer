using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "StaySober/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public Color Color;
    public int Life;
    public int AtkDamage;
    public float AtkSpeed;
}
