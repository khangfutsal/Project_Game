using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skeleton : Enemy
{
    public Skeleton_Data skeleton_Data;

    [Header("Current Position")]
    [SerializeField] private Vector3 curPos;


    [Header("Initialize Direction Moving For FlyingEye")]
    [SerializeField] protected Transform startPos;
    [SerializeField] protected Transform endPos;

    protected float _current;

    
}
