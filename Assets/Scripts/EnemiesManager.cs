using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;



public class EnemiesManager : MonoBehaviour
{
   
    private UIManager _UIManager;
    public EnemySpawner enemySpawner;
    [SerializeField] EnemySpawnList _enemySpawnList;
    Dictionary<SpawnZone, GameObject> MonsterToSpawns = new Dictionary<SpawnZone, GameObject>();
    List<EnemyAI> enemies = new List<EnemyAI>();
    [SerializeField] int spawnCountdown;


    private static EnemiesManager _instance;
    public static EnemiesManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new EnemiesManager();
            }

            return _instance;
        }
    }


    void Awake()
    {
      
        _instance = this;
    }
    private void Start()
    {
        _UIManager = UIManager.Instance;
        SetSpawns();
        InitEnemies();
    }
    void InitEnemies()
    {
        List<SpawnZone> spawnsList = new List<SpawnZone>();
        spawnsList.Add(SpawnZone.East);
        spawnsList.Add(SpawnZone.West);
        spawnsList.Add(SpawnZone.South);
        spawnsList.Add(SpawnZone.North);
        StartCoroutine(GameStartProcces(spawnsList,spawnCountdown));

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SpawnEnemy(SpawnZone.East);
        }
    }
    public void SpawnEnemy(SpawnZone spawnZone)
    {
        enemySpawner.CreateNewEnemy(MonsterToSpawns[spawnZone], spawnZone);
    }
    public void EnemySpawned(EnemyAI enemy)
    {

        
        enemies.Add(enemy);
        Debug.Log(enemies[0].hp + enemies[0].id);
        enemySpawner.nextID++;
    }
    public void removeEnemy(EnemyAI enemy)
    {

        enemies.Remove(enemy);
        Debug.Log("Enemy " + enemy.id + " Died..." + "   " + "Removing from enemiesList " + enemies.Count + " left");






    }

    public void enemyDied(EnemyAI enemy, SpawnZone deadZone)
    {
        removeEnemy(enemy);
        StartCoroutine(EnemyDeathProcces(deadZone,enemy.spawnTime));

    }


    public EnemyAI GetEnemyByID(int ID)
    {
        EnemyAI enemy;

        enemy = enemies.Where(x => x.id == ID).First();
        int[] array = new int[5];

        List<int> list = array.ToList();



        return enemy;
    }
    void SetSpawns()
    {

        MonsterToSpawns.Add(SpawnZone.North, _enemySpawnList.NorthEnemies);
        MonsterToSpawns.Add(SpawnZone.South, _enemySpawnList.SouthEnemies);
        MonsterToSpawns.Add(SpawnZone.East, _enemySpawnList.EastEnemies);
        MonsterToSpawns.Add(SpawnZone.West, _enemySpawnList.WestEnemies);
    }

    IEnumerator EnemyDeathProcces(SpawnZone zone,int spawnTime)
    {
        _UIManager.MonsterUiSwitch(true,zone);
        for (int i = 0; i < spawnTime; i++)
        {
            _UIManager.UseMonsterUI((spawnTime - i).ToString(), zone);
            
            yield return new WaitForSeconds(1);
        }
        
        SpawnEnemy(zone);
        _UIManager.MonsterUiSwitch(false,zone);


    }
    IEnumerator GameStartProcces(List<SpawnZone> spawnZones,int spawnTime)
    {
        foreach (SpawnZone found in spawnZones)
        {
            _UIManager.MonsterUiSwitch(true, found);
        }
            
        for (int i = 0; i < spawnTime; i++)
        {
            foreach (SpawnZone found in spawnZones)
            {
                _UIManager.UseMonsterUI((spawnTime - i).ToString(), found);
            }
                
            yield return new WaitForSeconds(1);
            

        }
        foreach(SpawnZone found in spawnZones)
        {
           SpawnEnemy(found);
           _UIManager.MonsterUiSwitch(false, found);
        }
      
    }
   
}
