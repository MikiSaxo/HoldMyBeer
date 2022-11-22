using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "EnemyData", menuName = "StaySober/Upgrades Data")]
public class UpgradesData : ScriptableObject
{
    public Sprite Icon;
    public string Description;
    [Header("Stats")]
    public float MovementSpeed;
    public float AtkSpeed;
    public int AtkDamage;
}
