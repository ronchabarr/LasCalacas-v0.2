using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{


    [Header("Speeds:")]
    public float moveSpeed;
    public float DashForce, crouchSpeed, airSpeed, jumpForce;
    public float attackSpeed;

    [Header("Attributes:")]
    public int maxHp;
    public int currentHp, stamina, shield, Level;
    public int CritChance;
    public int PhisycalDamage;
    public int AbilityDamage;

    [Header("InputSettings:")]
    public float mouseSensetivity;
    public float targetMoveSpeed;

    [Header("Attacks")]

    [Header("AttacksRanges:")]
    public float basicAttackRange;

    [Header("Abilitys")]
    public float ability1_CD;
    public float ability2_CD;
}

   



