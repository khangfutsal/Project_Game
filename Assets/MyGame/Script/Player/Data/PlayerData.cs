using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "newPlayerData",menuName = "Data/Player Data/Base Data")]
public class PlayerData : ScriptableObject
{
    [Header("Move State")]
    public float movementVelocity = 10f;


    [Header("Jump State")]
    public float jumpVelocity = 15f;
    public int amountOfJumps = 1;

    [Header("Attack State")]
    public int attackTimes = 3;
    public int knockOutX;
    public int knockOutY;
    public int knockDuration;

    [Header("Check Variables")]
    public float groundCheckRadius = .3f;
    public LayerMask whatIsGround;

    
}
