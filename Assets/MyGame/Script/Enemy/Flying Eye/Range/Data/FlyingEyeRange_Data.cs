using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class FlyingEyeRange_Data : FlyingEyeData
{
    [Range(0, 5)]
    public float attackRadius;

    public LayerMask whatIsPlayer;

    [Header("Attack State")]
    public float delayAttack;

    [Header("Grounded")]
    public float groundCheckRadius = .3f;
    public LayerMask whatIsGround;
}
