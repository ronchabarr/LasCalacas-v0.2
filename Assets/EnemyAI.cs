using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemyStats enemyStats;
    public EnemiesManager enemiesManager=new EnemiesManager();
<<<<<<< Updated upstream
 
=======
    MeshRenderer myMesh;
    Rigidbody rb;
   public Material[] mat;


>>>>>>> Stashed changes
    internal int hp,level,damage;
    internal int id;


    void Awake()
    {
        GetData();
    }

    void Start()
    {
<<<<<<< Updated upstream

=======
        myMesh = this.gameObject.GetComponent<MeshRenderer>();
        hp *= level;
        damage *= level;
        rb = GetComponent<Rigidbody>();
        
>>>>>>> Stashed changes
       
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
    public void ApplyKnockBack(float explosionStrenght,Vector3 Source,float radius)
    {

        rb.AddExplosionForce(explosionStrenght, Source, radius);


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

