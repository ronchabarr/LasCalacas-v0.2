using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemySpawner : MonoBehaviour
{
    
    Dictionary<SpawnZone, Transform> MonsterSpawnPoints = new Dictionary<SpawnZone, Transform>(); 
    EnemiesManager enemysManager;
    public GameObject[] enemyTypes;
    [Header("SpawnPoints:")]
    [SerializeField] Transform northPos;
    [SerializeField] Transform southPos;
    [SerializeField] Transform eastPos;
    [SerializeField] Transform westPos;
    [Header("SpawnCounterByZone:")]
    internal int nextID;
    
    
    void Start()
    {   enemysManager = EnemiesManager.Instance;
        enemysManager.enemySpawner = this;
        SetSpawnPoints();
        


    }


    private void Update()
    {

    }

    public void CreateNewEnemy(GameObject spawnEnemy,SpawnZone zoneToSpawn)
    {
      EnemyAI enemyAI;
     GameObject go = Instantiate(spawnEnemy,MonsterSpawnPoints[zoneToSpawn].position,Quaternion.identity);
     enemyAI = go.GetComponent<EnemyAI>();
     enemyAI.id = nextID;
     enemyAI._enemiesManager = enemysManager;
        enemyAI.spawnZone = zoneToSpawn;
     enemysManager.EnemySpawned(enemyAI);





    }
    void SetSpawnPoints()
    {
        MonsterSpawnPoints.Add(SpawnZone.North, northPos);
        MonsterSpawnPoints.Add(SpawnZone.South, southPos);
        MonsterSpawnPoints.Add(SpawnZone.East, eastPos);
        MonsterSpawnPoints.Add(SpawnZone.West, westPos);
        
            
    }

}
