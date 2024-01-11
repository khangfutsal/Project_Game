using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class SkeletonMelee_Data : Skeleton_Data
{
    public float moveSpeed;
    [Header("Attack State")]
    public float knockDuration;
}
