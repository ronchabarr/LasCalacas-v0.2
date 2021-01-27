using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;
    private Vector3 AI_currenctPos;
    private Vector3 startPos;
    private Transform TstartPos;
    public Transform playerpos;
    public GameObject projectile;
    public GameObject lastAttacker;
    private Rigidbody rb;
    public SpawnZone spawnZone;
    Animator anim;



    public EnemyStats enemyStats;
    public EnemiesManager _enemiesManager;
    Material myMat;
 
    [SerializeField] SkinnedMeshRenderer originalMat;
    [SerializeField] Material hitMat;



    //which layers should i notice?
    public LayerMask whatIsGround, whatIsTarget;

    //Patroling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerinSightRange, playerInAttackRange;
    public bool gotHit;
    internal bool gotOutOfRange;

    //Stats ( will be a singleTone stats) 
   
    internal float speed = 3.4f;
  
    
    internal float distance = 4f;
    internal int hp, level, damage,maxHp,spawnTime;
    internal int id;


    private void Awake()
    {
        _enemiesManager = EnemiesManager.Instance;
        startPos = gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        gotOutOfRange = false;
        rb = GetComponent<Rigidbody>();
        TstartPos = GetComponent<Transform>();
        TstartPos.transform.position = gameObject.transform.position;
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        
       
        GetData();
        myMat = originalMat.material;
       
      

        gotHit = false;

    }

    private void Update()
    {
        //Debug
        Debug.Log("[sight] " + playerinSightRange);
        Debug.Log("[attackrange] " + playerInAttackRange);
        Debug.Log("[outofRange]  " + gotOutOfRange);
        Debug.Log("[Hit] " + gotHit);


        //Grabbing on realtime AI pos
        AI_currenctPos = this.transform.position;

        //Making sure AI has playerPos
        playerpos = GameObject.Find("Bored").transform;


        //Check for sight and attack range
        playerinSightRange = Physics.CheckSphere(AI_currenctPos, sightRange, whatIsTarget);
        playerInAttackRange = Physics.CheckSphere(AI_currenctPos, attackRange, whatIsTarget);


        if (!gotHit || gotOutOfRange) Homming();
        if (playerinSightRange && !playerInAttackRange && !gotOutOfRange && gotHit) ChasePlayer();
       // if (playerinSightRange && playerInAttackRange && !gotOutOfRange && gotHit) AttackPlayer();

    }


    private void Homming()
    {
        //outofrangecheck
        if (gotOutOfRange)
        {
            gotOutOfRange = false;
            gotHit = false;
            hp = maxHp;

            agent.SetDestination(startPos);
             anim.SetBool("Chase", false) ;
        }




    }

    //private void SearchWalkPoint()
    //{
    //    //Calculate random points in range
    //    float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //    float randomX = Random.Range(-walkPointRange, walkPointRange);

    //    walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

    //    if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
    //        walkPointSet = true;
    //}
    private void ChasePlayer()
    {
        CheckMaxRange();
        if (hp < maxHp)
        {
            if (!gotOutOfRange)
            {
                agent.SetDestination(playerpos.position);
                anim.SetBool("Chase", true);
            }
            else
            {

               
            }
         

        }
    }

    private void CheckMaxRange()
    {
        if (Vector3.Distance(startPos, AI_currenctPos) < walkPointRange) gotOutOfRange = false;
        else gotOutOfRange = true;


    }

    private void AttackPlayer()
    {
        //make sure enemy doesn't move
        agent.SetDestination(transform.position);

        transform.LookAt(playerpos);

        if (!alreadyAttacked)
        {
            //attac code here
            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();


            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);


            ///
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        gotHit = true;
        hp -= damage;
       
        originalMat.material = hitMat;
        Invoke("colorBack", 0.2f);


        if (hp <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
    public void ApplyKnockBack(float explosionStrenght, Vector3 Source, float radius)
    {

        rb.AddExplosionForce(explosionStrenght, Source, radius);


    }
    private void OnDestroy()
    {
        if (_enemiesManager != null)
        {

        _enemiesManager.enemyDied(this,spawnZone);
        }



    }

    public void GetData()
    {
        hp = enemyStats.hp;
        damage = enemyStats.damage;
        level = enemyStats.level;
        maxHp = enemyStats.maxHp;
        spawnTime = enemyStats.spawnTime;


    }
    public void colorBack()
    {
       originalMat.material = myMat;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}








    







