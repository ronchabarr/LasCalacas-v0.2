using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Player player;
    Animator anim;
    

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
            player.stats.velocity += Time.deltaTime * player.stats.acceleration;
        }
        else
        {
            anim.SetBool("isWalking", false);
            player.stats.velocity -= Time.deltaTime * player.stats.deacceleration;
        }

        if (player.dash)
        {
            anim.SetTrigger("dash");

        }




        //atk Counter
        if (player.stats.isattackingOne)
        {
            player.stats.AtkOne += Time.deltaTime * player.stats.AtkOneacceleration;

        }
        else
        {
            if (player.stats.AtkOne > 0)
            {
                player.stats.AtkOne -= Time.deltaTime * player.stats.AtkOnedeacceleration;
            }
            if (player.stats.AtkOne < 0)
            {
                player.stats.AtkOne = 0;
            }
        }
        if (player.stats.AtkOne >= 1)
        {
            player.stats.isattackingOne = false;
        }
        anim.SetFloat("AtkOne", player.stats.AtkOne);


        //atk 2 Counter
        if (player.stats.isattackingTwo)
        {
            player.stats.AtkTwo += Time.deltaTime * player.stats.AtkTwoacceleration;

        }
        else
        {
            if (player.stats.AtkTwo > 0)
            {
                player.stats.AtkTwo -= Time.deltaTime * player.stats.AtkTwodeacceleration;
            }
            if (player.stats.AtkTwo < 0)
            {
                player.stats.AtkTwo = 0;
            }
        }
        if (player.stats.AtkTwo >= 1)
        {
            player.stats.isattackingTwo = false;
        }
        anim.SetFloat("AtkTwo", player.stats.AtkTwo);



        //skill one Counter
        if (player.stats.isUsingSkillOne)
        {
            player.stats.SkillOne += Time.deltaTime * player.stats.SkillOneacceleration;

        }
        else
        {
            if (player.stats.SkillOne > 0)
            {
                player.stats.SkillOne -= Time.deltaTime * player.stats.SkillOnedeacceleration;
            }
            if (player.stats.SkillOne < 0)
            {
                player.stats.SkillOne = 0;
            }
        }
        if (player.stats.SkillOne >= 1)
        {
            player.stats.isUsingSkillOne = false;
        }
        anim.SetFloat("SkillOne", player.stats.SkillOne);

        //skill Two Counter
        if (player.stats.isUsingSkillTwo)
        {
            player.stats.SkillTwo += Time.deltaTime * player.stats.SkillTwoacceleration;

        }
        else
        {
            if (player.stats.SkillTwo > 0)
            {
                player.stats.SkillTwo -= Time.deltaTime * player.stats.SkillTwodeacceleration;
            }
            if (player.stats.SkillTwo < 0)
            {
                player.stats.SkillTwo = 0;
            }
        }
        if (player.stats.SkillTwo >= 1)
        {
            player.stats.isUsingSkillTwo = false;
        }
        anim.SetFloat("SkillTwo", player.stats.SkillTwo);



        //player blend
        if (player.stats.velocity > 1)
        {
            player.stats.velocity = 1;
        }
        else if (player.stats.velocity < 0)
        {
            player.stats.velocity = 0;
        }

        anim.SetFloat("Velocity", player.stats.velocity);

        
        

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
                    player.stats.isattackingOne = true;
                    anim.SetFloat("attackSpeed", player.stats.attackSpeed);
                    anim.SetTrigger("attack1");
                }
                if (i == 1)
                {
                    player.stats.isattackingTwo = true;
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
                    player.stats.isUsingSkillOne = true;
                    anim.SetTrigger("skill1");
                }
                if (i == 1) 
                {
                    player.stats.isUsingSkillTwo = true;
                    anim.SetTrigger("skill2");
                }
                  

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
