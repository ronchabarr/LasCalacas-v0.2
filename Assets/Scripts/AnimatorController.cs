using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    Player player;
    Animator anim;
  
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
    public void UpdateAnimes()
    {
        //Movement Animations//

        if(player.isWalking)
        {
            player.StatsSO.velocity += player.StatsSO.acceleration * Time.deltaTime;
            if (player.StatsSO.velocity > 1)
            {
                player.StatsSO.velocity = 1;
            }

        }
        else
        {
            if (player.StatsSO.velocity > 0)
            {
                player.StatsSO.velocity -= player.StatsSO.deacceleration * Time.deltaTime;
            }

            if (player.StatsSO.velocity < 0)
            {
                player.StatsSO.velocity = 0;
            }

        }
        if (player.StatsSO.isattackingOne)
        {
            player.StatsSO.AtkOne += player.StatsSO.AtkOneacceleration * Time.deltaTime;
            if (player.StatsSO.AtkOne > 1)
            {
                player.StatsSO.AtkOne = 1;
                player.StatsSO.isattackingOne = false;
            }

        }
        else
        {
            if (player.StatsSO.AtkOne > 0)
            {
                player.StatsSO.AtkOne -= player.StatsSO.AtkOnedeacceleration * Time.deltaTime;
            }

            if (player.StatsSO.AtkOne < 0)
            {
                player.StatsSO.AtkOne = 0;
            }
        }


        if (player.StatsSO.isattackingTwo)
        {
            player.StatsSO.AtkTwo += player.StatsSO.AtkTwoacceleration * Time.deltaTime;
            if (player.StatsSO.AtkTwo > 1)
            {
                player.StatsSO.AtkTwo = 1;
                player.StatsSO.isattackingTwo = false;
            }

        }
        else
        {
            if (player.StatsSO.AtkTwo > 0)
            {
                player.StatsSO.AtkTwo -= player.StatsSO.AtkTwodeacceleration * Time.deltaTime;
            }

            if (player.StatsSO.AtkTwo < 0)
            {
                player.StatsSO.AtkTwo = 0;
            }
        }













        anim.SetFloat("Velocity", player.StatsSO.velocity);
        anim.SetFloat("AtkOne", player.StatsSO.AtkOne);
        anim.SetFloat("AtkTwo", player.StatsSO.AtkTwo);

        anim.SetBool("IsWalking", player.isWalking);
    }
    public void CommandAnimes()
    {

        if (player.dash && player.isAttackState)
        {
            anim.SetTrigger("dash");
        }


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
                    anim.SetFloat("attackSpeed", player.stats.attackSpeed);
                    anim.SetTrigger("attack1");
                    player.StatsSO.isattackingOne = true;
                }
             if (i==1)
                {
                    anim.SetTrigger("attack2");
                    player.StatsSO.isattackingTwo = true;
                }
             if (i==2)
                anim.SetTrigger("attack3");
            }

        }
    
        //Skills Animations//
        for (int i = 0; i < player.Skills.GetLength(0); i++)
        {
            if (player.Skills[i, 0] && !player.Skills[i, 1]) 
            {

            if (i==0)
                anim.SetTrigger("skill1");
            if (i==1)
                anim.SetTrigger("skill2");
            if (i==2)
                anim.SetTrigger("skill3");
            if (i==3)
                anim.SetTrigger("skill4");
            if (i==4)
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
