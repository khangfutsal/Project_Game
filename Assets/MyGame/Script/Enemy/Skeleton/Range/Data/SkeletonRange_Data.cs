using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkeletonRange_Data : Skeleton_Data
{
    [Range(0, 10)]
    public float attackRadius;

    public LayerMask whatIsPlayer;

    [Header("Move State")]
    public float moveSpeed;
    [Header("Attack State")]
    public float knockDuration;

}
