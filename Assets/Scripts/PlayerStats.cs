using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{


    [Header("Speeds:")]
    public float moveSpeed;
    public float runSpeed, crouchSpeed, airSpeed, jumpForce;
    
    [Header("Attributes:")]
    public int maxHp;
    public int currentHp, stamina, shield, Level;

    [Header("InputSettings:")]
    public float mouseSensetivity;

   


}
