using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Skeleton_Data : EnemyData
{
    [Header("Check Variables")]
    public float groundCheckRadius = .3f;
    public LayerMask whatIsGround;
}
