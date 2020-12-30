using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{

    //READ//
    //when adding value add transition in Player.PrintData()
    [Header("Speeds:")]
    public float moveSpeed;
    public float DashForce, crouchSpeed, airSpeed, jumpForce;
    public float attackSpeed;

    [Header("Attributes:")]
    public float lifeSteal;
    public int maxHp;
    public int currentHp, stamina, shield,  Level;
    public int CritChance;
    public int PhisycalDamage;
    public int AbilityDamage;

    [Header("InputSettings:")]
    public float mouseSensetivity;
    public float targetMoveSpeed;

    [Header("Attacks")]

    [Header("AttacksRanges:")]
    public float basicAttackRange;

    [Header("Skill1")]

    public float ability1_CD;
    public float ability1_Duration;
    public float ability1_Range;



    [Header("Skill2")]

    public float ability2_CD;
    public float ability2_Duration;
    public float ability2_Range;
    public ParticleSystem ability2_Effect;



    [Header("Skill3")]

    public float ability3_CD;
    public float ability3_Duration;
    public float ability3_Range;


    [Header("Skill4")]

    public float ability4_CD;
    public float ability4_Duration;
    public float ability4_Range;


}





