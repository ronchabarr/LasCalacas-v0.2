using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    public PlayerStats stats;
    GenericController genericController;
    AnimatorController animatorController;
    public GameObject tps;
    int skills = 5;
    int skillsStates = 2;
    
    internal bool[,] Attacks = new bool[3,2];
    internal bool[,] Skills = new bool[5,2];
    internal bool[] gestures = new bool[3];
    internal bool[] emotes = new bool[3];

    internal bool isAttackState;
    internal bool isRunning;
    internal bool jump;
    internal bool isWalking;
    internal bool isCrouching;
    internal bool isGrounded;
    internal bool doShield;
    


    Vector2 moveVector;

    //ADDED
    public GameObject emotePrefab; //has Emote script
    [SerializeField]
    Transform emoteSpawnPoint;

    public Sprite[] emoteSprites;
    public float emoteLifetime;


    //ADDED

    public void Start()
    {
        
        genericController = GetComponent<GenericController>();
        animatorController = GetComponent<AnimatorController>();

        rb = GetComponent<Rigidbody>();
    }

   

    private void FixedUpdate()
    {
        
       
       rb.AddRelativeForce(moveVector.x,0,moveVector.y,ForceMode.Impulse);
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            Debug.Log("detect");
        } 
    }






    public void Update()
    {
       
        InputForm _inputForm = genericController.GetInputForm();
        
        float _speed;

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
        if (_inputForm.runPress&!isAttackState)
        {
          
                _speed = stats.runSpeed;
                isRunning = true;

        }else isRunning = false;





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
            if (_inputForm.skills[i, 0] && !isAttackState)
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
     
       transform.Rotate(0, _inputForm.rotVector.x, 0);
        tps.transform.Rotate(-_inputForm.rotVector.y,0,0);

        

        animatorController.Animate();
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

    IEnumerator Attack1()
    {
        isAttackState = true;
        Attacks[0,0] = true;
        yield return new WaitForEndOfFrame();
        Attacks[0,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }  
    IEnumerator Attack2()
    {
        isAttackState = true;
        Attacks[1,0] = true;
        yield return new WaitForEndOfFrame();
        Attacks[1,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }  
    IEnumerator Attack3()
    {
        isAttackState = true;
        Attacks[2,0] = true;
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
        yield return new WaitForEndOfFrame();
        Skills[0,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Skill2()
    {
        isAttackState = true;
        Skills[1,0] = true;
        yield return new WaitForEndOfFrame();
        Skills[1,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Skill3()
    {
        isAttackState = true;
        Skills[2,0] = true;
        yield return new WaitForEndOfFrame();
        Skills[2,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Skill4()
    {
        isAttackState = true;
        Skills[3,0] = true;
        yield return new WaitForEndOfFrame();
        Skills[3,0] = false;
        yield return new WaitForSeconds(2);
        isAttackState = false;
        yield return null;
    }
    IEnumerator Skill5()
    {
        isAttackState = true;
        Skills[4,0] = true;
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
        yield return new WaitForEndOfFrame();
        doShield = false;
        yield return new WaitForSeconds(1);
        isAttackState = false;
    }
    IEnumerator Gesture1()
    {
        isAttackState = true;
        gestures[0] = true;
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





}


public class InputForm
{
    public Vector2 moveVector;
    public Vector2 rotVector; //x -player y rotation || y -camera rig x rotation

    public bool runPress,crouchPress,jumpHold,jumpPress,attackState,
        interractPress,interractHold,shieldPress,shieldHold,menuPress,backPress,reload,charge                 ;
    
    public bool[,] skills = new bool[5,2]; //[x,0] - press array || [x,1] - hold array || x-skill type
    public bool[,] attacks = new bool[3,2]; //[x,0] - press array || [x,1] - hold array || x-attack type
    public bool[] gesture = new bool[3];
    public bool[] emote = new bool[3];
    










}




    