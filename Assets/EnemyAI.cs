using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyStats enemyStats;
    public EnemiesManager enemiesManager=new EnemiesManager();
 
    internal int hp,level,damage;
    internal int id;


    void Awake()
    {
        GetData();
    }

    void Start()
    {

       
    }
   
    
    void Update()
    {
        
        if (hp < 100)
        {
            Destroy(this.gameObject);
        }

      
    }

    public void GetHit(int Damage)
    {
        hp -= Damage;
        
       

    } 
    public void Attack()
    {
        
    }
    private void OnDestroy()
    {
        enemiesManager.enemyDied(this);
        
        
        
    }
    public void GetData()
    {
        hp = enemyStats.hp;
        damage = enemyStats.damage;
        
        
    }

}

