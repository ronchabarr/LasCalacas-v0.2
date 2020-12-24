using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Player player;
    Animator anim;
    float velocity = 0.0f;
    public float acceleration;
    public float deacceleration;
    float AtkOne;
    public float AtkOneacceleration;
    public float AtkOnedeacceleration;
    public bool isattackingOne = false;
    float AtkTwo;
    public float AtkTwoacceleration;
    public float AtkTwodeacceleration;
    public bool isattackingTwo = false;
    float SkillOne;
    public float SkillOneacceleration;
    public float SkillOnedeacceleration;
    public bool isUsingSkillOne = false;
    float SkillTwo;
    public float SkillTwoacceleration;
    public float SkillTwodeacceleration;
    public bool isUsingSkillTwo = false;

    [SerializeField]

    // Start is called before the first frame update
    void Start()
    {

        player = GetComponent<Player>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       







    }



    public void Animate()
    {

        //Movement Animations//
        if (player.isWalking)
        {
            anim.SetBool("isWalking", true);
            velocity += Time.deltaTime * acceleration;
        }
        else
        {
            anim.SetBool("isWalking", false);
            velocity -= Time.deltaTime * deacceleration;
        }

        if (player.dash)
        {
            anim.SetTrigger("dash");

        }




        //atk Counter
        if (isattackingOne)
        {
            AtkOne += Time.deltaTime * AtkOneacceleration;

        }
        else
        {
            if (AtkOne > 0)
            {
                AtkOne -= Time.deltaTime * AtkOnedeacceleration;
            }
            if (AtkOne < 0)
            {
                AtkOne = 0;
            }
        }
        if (AtkOne >= 1)
        {
            isattackingOne = false;
        }
        anim.SetFloat("AtkOne", AtkOne);


        //atk 2 Counter
        if (isattackingTwo)
        {
            AtkTwo += Time.deltaTime * AtkTwoacceleration;

        }
        else
        {
            if (AtkTwo > 0)
            {
                AtkTwo -= Time.deltaTime * AtkTwodeacceleration;
            }
            if (AtkTwo < 0)
            {
                AtkTwo = 0;
            }
        }
        if (AtkTwo >= 1)
        {
            isattackingTwo = false;
        }
        anim.SetFloat("AtkTwo", AtkTwo);



        //skill one Counter
        if (isUsingSkillOne)
        {
            SkillOne += Time.deltaTime * SkillOneacceleration;

        }
        else
        {
            if (SkillOne > 0)
            {
                SkillOne -= Time.deltaTime * SkillOnedeacceleration;
            }
            if (SkillOne < 0)
            {
                SkillOne = 0;
            }
        }
        if (SkillOne >= 1)
        {
            isUsingSkillOne = false;
        }
        anim.SetFloat("SkillOne", SkillOne);

        //skill Two Counter
        if (isUsingSkillOne)
        {
            SkillOne += Time.deltaTime * SkillOneacceleration;

        }
        else
        {
            if (SkillOne > 0)
            {
                SkillOne -= Time.deltaTime * SkillOnedeacceleration;
            }
            if (SkillOne < 0)
            {
                SkillOne = 0;
            }
        }
        if (SkillOne >= 1)
        {
            isUsingSkillOne = false;
        }
        anim.SetFloat("SkillOne", SkillOne);



        //player blend
        if (velocity > 1)
        {
            velocity = 1;
        }
        else if (velocity < 0)
        {
            velocity = 0;
        }

        anim.SetFloat("Velocity", velocity);

        
        

        //Jump Animation//
        if (player.jump)
        {
            anim.SetTrigger("jump");
        }

        if (player.doShield)
        {
            anim.SetTrigger("shield");
        }

        //Attacks Animations//
        for (int i = 0; i < player.Attacks.GetLength(0); i++)
        {
            if (player.Attacks[i, 0])
            {
                if (i == 0)
                {
                    isattackingOne = true;
                    anim.SetFloat("attackSpeed", player.stats.attackSpeed);
                    anim.SetTrigger("attack1");
                }
                if (i == 1)
                {
                    isattackingTwo = true;
                    anim.SetTrigger("attack2");
                }
                if (i == 2)
                    anim.SetTrigger("attack3");
            }

        }

        //Skills Animations//
        for (int i = 0; i < player.Skills.GetLength(0); i++)
        {
            if (player.Skills[i, 0])
            {

                if (i == 0)
                {
                    isUsingSkillOne = true;
                    anim.SetTrigger("skill1");
                }
                if (i == 1)
                    anim.SetTrigger("skill2");
                if (i == 2)
                    anim.SetTrigger("skill3");
                if (i == 3)
                    anim.SetTrigger("skill4");
                if (i == 4)
                    anim.SetTrigger("skill5");
            }
        }

        for (int i = 0; i < player.gestures.Length; i++)
        {
            if (player.gestures[i])
            {
                if (i == 0)
                    anim.SetTrigger("gesture1");
                if (i == 1)
                    anim.SetTrigger("gesture2");
                if (i == 2)
                    anim.SetTrigger("gesture3");
            }
        }

        for (int i = 0; i < player.emotes.Length; i++)
        {
            if (player.emotes[i])
            {
                if (i == 0)
                    anim.SetTrigger("emote1");
                if (i == 1)
                    anim.SetTrigger("emote2");
                if (i == 2)
                    anim.SetTrigger("emote3");
            }
        }

    }

}
