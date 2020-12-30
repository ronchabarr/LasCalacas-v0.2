using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_bigMonster : MonoBehaviour
{
    public NavMeshAgent agent;

    public Vector3 AI_currenctPos;

    public Vector3 startPos;

    private Transform TstartPos;

    public Transform playerpos;

    public GameObject projectile;
    public GameObject lastAttacker;

    private Rigidbody rb;

    //Gathering out side information
    private AI_maxRange canChase;
    ///
       

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
    public float currentHealth = 100f;
    internal float speed = 3.4f;
    internal float damage = 30f;
    internal float maxHealth = 100f;
    internal float distance = 4f;

    private void Awake()
    {
        startPos = gameObject.transform.position;
        agent = GetComponent<NavMeshAgent>();
        gotOutOfRange = false;
        rb = GetComponent<Rigidbody>();
        TstartPos = GetComponent<Transform>();
        TstartPos.transform.position = gameObject.transform.position;
    }

    private void Start()
    {
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
        if (playerinSightRange && playerInAttackRange && !gotOutOfRange && gotHit) AttackPlayer();

    }


    private void Homming()
    {
        //outofrangecheck
        if (gotOutOfRange)
        {
            gotOutOfRange = false;
            gotHit = false;
            currentHealth = maxHealth;

            agent.SetDestination(startPos);
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
        if (currentHealth < maxHealth)
        {
            if (!gotOutOfRange)
            {
              agent.SetDestination(playerpos.position);
            }

        }
    }

    private void CheckMaxRange()
    {
        if (Vector3.Distance(startPos, AI_currenctPos) < distance) gotOutOfRange = false;
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
        currentHealth -= damage;

        if (currentHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
