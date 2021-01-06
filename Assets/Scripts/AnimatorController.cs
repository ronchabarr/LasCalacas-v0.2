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
    public void SlowMotionAnim(float rythem)
    {

        anim.SetFloat("slowMo", rythem);
    }
    public void UpdateAnimes()
    {
        //Movement Animations//
        if (player.isWalking)
        {
            anim.SetBool("isWalking", true);
        }
        else anim.SetBool("isWalking", false);


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
            if (player.Attacks[i,0])
            {
                if (i == 0)
                    anim.SetFloat("attackSpeed",player.stats.attackSpeed);
                anim.SetTrigger("attack1");
             if (i==1)
                anim.SetTrigger("attack2");
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
                if (i == 3)

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
