using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject Target;
    Rigidbody rb;
    public PlayerStats StatsSO;
    public CharacterShit stats;
    GenericController _genericController;
    AnimatorController _animatorController;
    public GameObject tps;
    int skills = 5;
    int skillsStates = 2;
    float _speed;
    public Camera myCam;

    internal bool[,] Attacks = new bool[3,2];
    internal bool[,] Skills = new bool[5,2];
    internal bool[] gestures = new bool[3];
    internal bool[] emotes = new bool[3];

    internal bool isAttackState;
    internal bool dash;
    internal bool jump;
    internal bool isWalking;
    internal bool isCrouching;
    internal bool isGrounded;
    internal bool doShield;

    public LayerMask enemyLayerMask;

    public ParticleSystem basicAttackPS;
    public ParticleSystem skill2PS;
    public ParticleSystem[] skill3PS;
    public ParticleSystem skill1PS;
    public EarthCrackSkill earthCrackSkill;
    public GameObject GroundSmackskill;
    public Vector2 moveVector;

    //ADDED
    public GameObject emotePrefab; //has Emote script
    [SerializeField]
    Transform emoteSpawnPoint;

    public Sprite[] emoteSprites;
    public float emoteLifetime;


    [Header("AttackSources:")]
    public Transform basicAttackSource;


    //ADDED

    public void Start()
    {
        
        _genericController = GetComponent<GenericController>();
        _animatorController = GetComponent<AnimatorController>();
        CharacterShit _characterShit= new CharacterShit();
        PrintData();
        
        
        rb = GetComponent<Rigidbody>();
    }

    public void PrintData()
    {
        stats.ability1_CD = StatsSO.ability1_CD;
        stats.ability2_CD = StatsSO.ability2_CD;
        stats.AbilityDamage = StatsSO.AbilityDamage;
        stats.airSpeed = StatsSO.airSpeed;
        stats.attackSpeed = StatsSO.attackSpeed;
        stats.basicAttackRange = StatsSO.basicAttackRange;
        stats.CritChance = StatsSO.CritChance;
        stats.crouchSpeed = StatsSO.crouchSpeed;
        stats.currentHp = StatsSO.currentHp;
        stats.DashForce = StatsSO.DashForce;
        stats.jumpForce = StatsSO.jumpForce;
        stats.Level = StatsSO.Level;
        stats.lifeSteal= StatsSO.lifeSteal;
        stats.maxHp= StatsSO.maxHp;
        stats.mouseSensetivity= StatsSO.mouseSensetivity;
        stats.moveSpeed = StatsSO.moveSpeed;
        stats.PhisycalDamage= StatsSO.PhisycalDamage;
        stats.shield = StatsSO.shield;
        stats.stamina = StatsSO.stamina;
        stats.targetMoveSpeed= StatsSO.targetMoveSpeed;
        stats.ability1_CD = StatsSO.ability1_CD;
        stats.ability1_Duration = StatsSO.ability1_Duration;
        stats.ability1_Range = StatsSO.ability1_Range;
        stats.ability2_CD = StatsSO.ability2_CD;
        stats.ability2_Duration = StatsSO.ability2_Duration;
        stats.ability2_Range = StatsSO.ability2_Range;
        stats.ability2_Effect = StatsSO.ability2_Effect;
        stats.ability3_CD = StatsSO.ability3_CD;
        stats.ability3_Duration = StatsSO.ability3_Duration;
        stats.ability3_Range = StatsSO.ability3_Range;
        stats.ability4_CD = StatsSO.ability4_CD;
        stats.ability4_Duration = StatsSO.ability4_Duration;
        stats.ability4_Range = StatsSO.ability4_Range;




    }

    private void FixedUpdate()
    {

        
       rb.AddRelativeForce(moveVector.x,0,moveVector.y,ForceMode.Impulse);
        dash = false;

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("Ground Detection");
        } 
    }
    public void CameraShake(float lenght,float strenght)
    {
        CameraShake camShake = myCam.GetComponent<CameraShake>();
        StartCoroutine(camShake.Shake(lenght, strenght));
    }






    public void Update()
    {
       
        InputForm _inputForm = _genericController.GetInputForm();
        
        

        if (_inputForm.moveVector != Vector2.zero&&!isAttackState)
        {
            _speed = stats.moveSpeed;
            isWalking = true;
        }
        else
        {
            _speed = 0;
            isWalking = false;
        }
        if (_inputForm.Dash&!isAttackState&!dash)
        {

            StartCoroutine(Dash());

        }





        if (_inputForm.jumpPress)
        {
            jump = true;
            rb.AddForce(Vector3.up * stats.jumpForce);
        }
        else jump = false;

        //Attacks//
        for (int i = 0; i < _inputForm.attacks.GetLength(0); i++)
        {
            if (!isAttackState)
            {

                if (_inputForm.attacks[i, 0])
                {
                    AttackByIndex(i);
                    break;
                }
            }

        }

       //Skills//
        for (int i = 0; i < _inputForm.skills.GetLength(0); i++)
        {
            if (_inputForm.skills[i, 0] && !isAttackState && !Skills[i,1])
            {
                if (i == 0)
                    StartCoroutine(Skill1());
                if (i == 1)
                    StartCoroutine(Skill2());
                if (i == 2)
                    StartCoroutine(Skill3());
                if (i == 3)
                    StartCoroutine(Skill4());
                if (i == 4)
                    StartCoroutine(Skill5());
                break;
            }

        }
        //Gestures//
        for (int i = 0; i < _inputForm.gesture.Length; i++)
        {
            if (_inputForm.gesture[i] && !isAttackState)
            {
                if (i == 0)
                    StartCoroutine(Gesture1());
                if (i == 1)
                    StartCoroutine(Gesture2());
                if (i == 2)
                    StartCoroutine(Gesture3());

                break;
            }

        }
        //Emotes//




        //_inputForm.emote.Where(x => x == true).First();
        for (int i = 0; i < _inputForm.emote.Length; i++)
        {
            if (_inputForm.emote[i] && !isAttackState)
            {
                Emote(i);
                break;
                //if (i == 0)
                //    StartCoroutine(Emote1());
                //if (i == 1)
                //    StartCoroutine(Emote2());
                //if (i == 2)
                //    StartCoroutine(Emote3());
            }

        }

        if (_inputForm.shieldPress && !isAttackState)
        {
            StartCoroutine(ShieldPress());
        }
       











        moveVector = _inputForm.moveVector * _speed;

        _animatorController.UpdateAnimes();

         transform.Rotate(0, _inputForm.rotVector.x, 0);
        tps.transform.Rotate(-_inputForm.rotVector.y*stats.mouseSensetivity,0,0);
     
        Target.transform.localPosition = new Vector3(Target.transform.localPosition.x, Target.transform.localPosition.y, Target.transform.localPosition.z + _inputForm.rotVector.y/(stats.mouseSensetivity* stats.targetMoveSpeed));



        
    }
    public IEnumerator LeapTowards(Transform destination)
    {
            
        for (int i = 0; i < 80; i++)
        {
            if (i > 40&&i<60)
            {

                transform.Translate((destination.localPosition * 2) / 80);
                yield return new WaitForSeconds(0.3f / (80 - i));
            }
            else
            {

                transform.Translate((destination.localPosition * 2) / 80);
                yield return new WaitForSeconds(0.1f / (80 - i));
            }
               
            

            
        }
        yield return null;
           
    }

    private void AttackByIndex(int i)
    {
        switch (i)
        {
            case 0:

                StartCoroutine(Attack1());
                break;

            case 1:

                StartCoroutine(Attack2());
                break;

            case 2:

                StartCoroutine(Attack3());
                break;
        }
    }
    IEnumerator Dash()
    {
        dash = true;
        isAttackState = true;
        _animatorController.CommandAnimes();
        rb.AddRelativeForce(Vector3.forward*1000);
        yield return new WaitForSeconds(0.6f);
        isAttackState = false;
        dash = false;
     
        yield return null;
    }

    IEnumerator Attack1()
    {
        float secondsInIE = 0;
        isAttackState = true;
        Attacks[0,0] = true;
        _animatorController.CommandAnimes();
        int DamageCount;
        DamageCount = stats.PhisycalDamage;
        if (DoCrit())
        {   //just for presenting!!
            basicAttackPS.Play();
            DamageCount *= 2;
         
        }
        secondsInIE++;
        yield return new WaitForSeconds(0.7f);
        Collider[] hit = Physics.OverlapSphere(basicAttackSource.position, stats.basicAttackRange,enemyLayerMask);
        foreach(Collider found in hit)
        {
           EnemyAI enemyhitted = found.GetComponent<EnemyAI>();
           

            
            enemyhitted.TakeDamage(DamageCount);

        }
        yield return new WaitForEndOfFrame();
        Attacks[0,0] = false;
        yield return new WaitForSeconds((2f/stats.attackSpeed)-secondsInIE);
        isAttackState = false;
        yield return null;
    }  
    IEnumerator Attack2()
    {
        isAttackState = true;
        Attacks[1,0] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForEndOfFrame();
        Attacks[1,0] = false;
        yield return new WaitForSeconds(2f);
        isAttackState = false;
        yield return null;
    }  
    IEnumerator Attack3()
    {
        isAttackState = true;
        Attacks[2,0] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForEndOfFrame();
        Attacks[2,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
        
    }
    IEnumerator Skill1()
    {
        isAttackState = true;
        Skills[0,0] = true;
        _animatorController.CommandAnimes();
       
        Buff(0.25f, 0.25f, 0.15f, stats.currentHp / 4,0 , true);
        yield return new WaitForEndOfFrame();
        Skills[0, 0] = false;
        Skills[0, 1] = true;

        skill1PS.Play();
        yield return new WaitForSeconds(2 / stats.attackSpeed);
        isAttackState = false;
        yield return new WaitForSeconds(15);
        Buff(0.25f, 0.25f, 0.15f, 0, stats.currentHp / 4, false);
        yield return new WaitForSeconds(5);
        Skills[0, 1] = false;

        yield return null;
    }

    IEnumerator Skill2()
    {
        float secondsInIE=0;
        int damageCount;
        damageCount = stats.AbilityDamage;
        isAttackState = true;
        Skills[1,0] = true;
        _animatorController.CommandAnimes();
        Skills[1, 1] = true;
        yield return new WaitForEndOfFrame();
        Skills[1, 0] = false;
        skill2PS.Play();
        
        for (int i = 0; i < stats.ability2_Duration; i++)
        {
            
            Collider[] hit = Physics.OverlapSphere(transform.position, stats.ability2_Range, enemyLayerMask);
            foreach (Collider found in hit)
            {
                EnemyAI enemyhitted = found.GetComponent<EnemyAI>();



                enemyhitted.TakeDamage(damageCount);
            }
            secondsInIE++;
            yield return new WaitForSeconds(1);
        }



        Debug.Log("aaaaas");
        isAttackState = false;
        yield return new WaitForSeconds(stats.ability2_CD - secondsInIE);
        Skills[1, 1] = false;
        yield return null;
    }
    IEnumerator Skill3()
    {
        isAttackState = true;
        Skills[2,0] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForSeconds(1);
        earthCrackSkill.Init();
        CameraShake(1f, 0.5f);
        for (int i = 0; i < skill3PS.Length; i++)
        {

            skill3PS[i].Play();
        }
        
        yield return new WaitForEndOfFrame();
        Skills[2,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Skill4()
    {
        float CastToMovementDelay= 0.6f;
        float secondsInIE = 0;
        int damageCount;
        damageCount = stats.AbilityDamage;
        isAttackState = true;
        Skills[3,0] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForSeconds(CastToMovementDelay);
        StartCoroutine(LeapTowards(Target.transform));
        _animatorController.SlowMotionAnim(2.2f);
        yield return new WaitForSeconds(0.2f);
        _animatorController.SlowMotionAnim(0.25f);
        yield return new WaitForSeconds(0.5f);
        _animatorController.SlowMotionAnim(3.5f);
       yield return new WaitForSeconds(0.3f);
        _animatorController.SlowMotionAnim(1);
        GameObject prefab = Instantiate(GroundSmackskill,transform.position,Quaternion.identity);
        prefab.GetComponent<GroundSmackEffect>().Init();
        
     //  prefab.transform.DetachChildren();

        for (int i = 0; i < stats.ability4_Duration; i++)
        {

            Collider[] hit = Physics.OverlapSphere(transform.position, stats.ability4_Range, enemyLayerMask);
            foreach (Collider found in hit)
            {
                EnemyAI enemyhitted = found.GetComponent<EnemyAI>();
                enemyhitted.ApplyKnockBack(1000, transform.position, 6.5f);
                enemyhitted.TakeDamage(damageCount);
            }
            secondsInIE++;
        }

        CameraShake(1f,4f);
        yield return new WaitForEndOfFrame();
        Skills[3,0] = false;
        yield return new WaitForSeconds(3);
        Destroy(prefab);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Skill5()
    {
        isAttackState = true;
        Skills[4,0] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForEndOfFrame();
        Skills[4,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator ShieldPress()
    {
        isAttackState = true;
        doShield = true;
        _animatorController.CommandAnimes();
        yield return new WaitForEndOfFrame();
        doShield = false;
        yield return new WaitForSeconds(1);
        isAttackState = false;
    }
    IEnumerator Gesture1()
    {
        isAttackState = true;
        gestures[0] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForEndOfFrame();
        gestures[0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Gesture2()
    {
        isAttackState = true;
        gestures[1] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForEndOfFrame();
        gestures[1] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Gesture3()
    {
        isAttackState = true;
        gestures[2] = true;
        _animatorController.CommandAnimes();
        yield return new WaitForEndOfFrame();
        gestures[2] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
   

    void Emote(int i)
    {
        GameObject go = Instantiate(emotePrefab, emoteSpawnPoint);
        Emote emote = go.GetComponent<Emote>();

        emote.SetEmote(emoteSprites[i]);
    }


    public bool DoCrit()
    {
        int CritCount = UnityEngine.Random.Range(1, 100);
    
        if (stats.CritChance >= CritCount)
        {
            return true;
        }
        return false;
    }




    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(basicAttackSource.position, stats.basicAttackRange);
        
    }

    //if no multiply multiply by 1 || if no add add 0 bool is for debuffing
    public void Buff(float attackSpeedM, float speedM, float lifeStealM, int Shield ,int heal,bool buff)
    {
        switch (buff)
        {
            case true:
            
                stats.attackSpeed += attackSpeedM;
                stats.moveSpeed += speedM;
                stats.lifeSteal += lifeStealM;
                stats.shield += Shield;
                stats.currentHp += heal;
            
               break;

            case false:
                stats.attackSpeed -= attackSpeedM;
                stats.moveSpeed -= speedM;
                stats.lifeSteal -= lifeStealM;
                stats.shield = Shield;
                stats.currentHp = heal;


                break;

        }

        
    }
}



public class InputForm
{
    public Vector2 moveVector;
    public Vector2 rotVector; //x -player y rotation || y -camera rig x rotation

    public bool Dash,crouchPress,jumpHold,jumpPress,attackState,
        interractPress,interractHold,shieldPress,shieldHold,menuPress,backPress,reload,charge                 ;
    
    public bool[,] skills = new bool[5,2]; //[x,0] - press array || [x,1] - hold array || x-skill type
    public bool[,] attacks = new bool[3,2]; //[x,0] - press array || [x,1] - hold array || x-attack type
    public bool[] gesture = new bool[3];
    public bool[] emote = new bool[3];
    







        



}





    