using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class EnemyAI : MonoBehaviour
{

    public SpawnZone spawnZone;
    public Transform playerpos;
    public GameObject projectile;
    public GameObject lastAttacker;
    public EnemyStats enemyStats;

    public LayerMask whatIsGround, whatIsTarget;
    public bool playerinSightRange, playerInAttackRange;
    public bool gotHit;
    public float sightRange, attackRange;
    public float walkPointRange;
    public float timeBetweenAttacks;


    [SerializeField] SkinnedMeshRenderer originalMat;
    [SerializeField] Material hitMat;

    internal EnemiesManager _enemiesManager;
    internal bool gotOutOfRange;
    internal float speed = 3.4f;
    internal float distance = 4f;
    internal int hp, level, damage,maxHp,spawnTime;
    internal int id;


    private Rigidbody rb;
    private Animator anim;
    private Material myMat;
    private NavMeshAgent agent;
    private Transform TstartPos;
    private Vector3 startPos;
    private bool alreadyAttacked;




    private void Awake()
    {
        SetVars();
        SetComponents();
    }
    public void SetVars()
    {
        _enemiesManager = EnemiesManager.Instance;
        startPos = transform.position;
       
    }
    public void SetComponents()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        GetData();
        myMat = originalMat.material;
        gotHit = false;
    }

    private void Update()
    {
        playerpos = GameObject.Find("Bored").transform;
        //Debug
        //Debug.Log("[sight] " + playerinSightRange);
        //Debug.Log("[attackrange] " + playerInAttackRange);
        //Debug.Log("[outofRange]  " + gotOutOfRange);
        //Debug.Log("[Hit] " + gotHit);
        //Check for sight and attack range
        if (!gotHit || gotOutOfRange) StartCoroutine(Homming());
        if (CheckSight() && !CheckAttRange() && !gotOutOfRange && gotHit) ChasePlayer();
       // if (playerinSightRange && playerInAttackRange && !gotOutOfRange && gotHit) AttackPlayer();
    }
    private bool homingflag = false;
    private IEnumerator Homming()
    {
        if (!homingflag)
        {
            homingflag = true;
            gotOutOfRange = false;
            gotHit = false;
            hp = maxHp;
        }
        while (Vector3.Distance(transform.position,startPos)>1)
        {
            agent.SetDestination(startPos);
            anim.SetBool("Chase", true);
            yield return new WaitForSeconds(0.1f);
            Debug.Log("inloop");
        }
        transform.LookAt(playerpos);
        anim.SetBool("Chase", false);
        homingflag = false;
    }
    
    

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
        if (Vector3.Distance(startPos, transform.position) < walkPointRange) gotOutOfRange = false;
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
        HitEffect();
      
       
        ApplyKnockBack(damage*30, playerpos.position, 10);
        
        

        if (hp <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }
    public IEnumerator SetAgent(bool turnOn,float Delay)
    {
        yield return new WaitForSeconds(Delay);
        if (turnOn)
        {
          
            agent.isStopped = false;
        }
        else
        {
            agent.isStopped = true;
       
        }
    }
  
    public IEnumerator SetKinematic(bool turnOn, float Delay)
    {
        yield return new WaitForSeconds(Delay);
        if (turnOn)
        {
            rb.isKinematic = true;
           
        }
        else
        {
           
            rb.isKinematic = false;
        }
    }

    private void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }
    public void ApplyKnockBack(float explosionStrenght, Vector3 Source, float radius)
    {
       StartCoroutine(SetKinematic(false, 0));
        StartCoroutine(SetAgent(false, 0));

        rb.AddExplosionForce(explosionStrenght, Source, radius);
        StartCoroutine(SetKinematic(true, 2));
        StartCoroutine(SetAgent(true, 2));
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
    public void HitEffect()
    {
        originalMat.material = hitMat;
        Invoke("colorBack", 0.2f);
    }
    public void colorBack()
    {
       originalMat.material = myMat;
    }
    public void ResetBattle()
    {

    }
    public bool CheckSight()
    {     
        return Physics.CheckSphere(transform.position, sightRange, whatIsTarget);
    } 
    public bool CheckAttRange()
    {
        return Physics.CheckSphere(transform.position, attackRange, whatIsTarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}








    







