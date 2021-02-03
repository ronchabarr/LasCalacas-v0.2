using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SpawnZone { North, South, East, West }
[CreateAssetMenu]
public class EnemySpawnList : ScriptableObject
{

    [Header("SpawnPoints")]

    [Header("North")]
    public GameObject NorthEnemies;
    [Header("South")]
    public GameObject SouthEnemies;
    [Header("East")]
    public GameObject EastEnemies;
    [Header("West")]
    public GameObject WestEnemies;





 
}
