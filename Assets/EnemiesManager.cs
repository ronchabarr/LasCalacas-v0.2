using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemiesManager 
{
    
    public EnemySpawner enemySpawner;
    List<EnemyAI> enemies = new List<EnemyAI>();
  


 





    public void EnemySpawned(EnemyAI enemy)
    {


        enemies.Add(enemy);
        Debug.Log(enemies[0].hp + enemies[0].id);
        enemySpawner.nextID++;
    }
    public void removeEnemy(EnemyAI enemy)
    {
        
        enemies.Remove(enemy);
        Debug.Log("Enemy " + enemy.id + " Died..." + "   " +"Removing from enemiesList "+ enemies.Count +" left");
        
        

      
        
        
    }
 
    public void enemyDied(EnemyAI enemy)
    {
        removeEnemy(enemy);
    }

    
    public EnemyAI GetEnemyByID(int ID)
    {
        EnemyAI enemy;

        enemy = enemies.Where(x => x.id == ID).First();
        int[] array = new int[5];

        List<int> list = array.ToList();

        

        return enemy;
    }
   
}

/*public class SomethingClass
{
    //public SomethingClass()
    //{
    //    shit = 1;
    //    fake = 8;
    //}
    public SomethingClass(int s, int f)
    {
        shit = s;
        fake = f;


        dsa();
    }
     void dsa()
    {

    }

    public int shit;
    public int fake;
}*/

