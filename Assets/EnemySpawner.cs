using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    
    EnemiesManager enemysManager;
    public GameObject[] enemyTypes;
    public Transform spawnPos;
    internal int nextID;
    

    void Start()
    {   enemysManager = new EnemiesManager();
        enemysManager.enemySpawner = this;

        //alon
        /*List<SomethingClass> somethingClasses = new List<SomethingClass>();

        somethingClasses.Add(new SomethingClass(10, 2));
        somethingClasses.Add(new SomethingClass(24, 2));
        somethingClasses.Add(new SomethingClass(51, 2));
        somethingClasses.Add(new SomethingClass(23, 2));
        somethingClasses.Add(new SomethingClass(12, 2));

        somethingClasses.Sort(SortOrder);

        foreach (var something in somethingClasses)
        {
            Debug.Log(something.shit.ToString() + " is my shitscore");
        }

        }
        public int SortOrder(SomethingClass a, SomethingClass b)
        {
        if (a.shit > b.shit)
            return -1;
        else if (a.shit == b.shit)
            return 0;
        else
            return 1;*/

    }
    //alon

    private void Update()
    {

    
        
       
     if (Input.GetKeyDown(KeyCode.J))
     {


      CreateNewEnemy();
     }
    }

    public void CreateNewEnemy()
    {
      EnemyAI enemyAI;
     GameObject go = Instantiate(enemyTypes[Random.Range(0,enemyTypes.Length)],spawnPos.position,Quaternion.identity);
     enemyAI = go.GetComponent<EnemyAI>();
     enemyAI.id = nextID;
     enemyAI.enemiesManager = enemysManager;





     enemysManager.EnemySpawned(enemyAI);
    }

}
