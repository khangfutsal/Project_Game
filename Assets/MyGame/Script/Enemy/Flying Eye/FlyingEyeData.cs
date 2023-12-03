using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEyeData : EnemyData
{
    [Header("Attack")]
    [Range(0, 5)]
    public float radAttack;
    [Range(0, 10)]
    public float moveSpeed;
}
