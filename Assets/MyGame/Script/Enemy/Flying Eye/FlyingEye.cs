using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FlyingEye : Enemy
{
    public FlyingEyeData flyingEye_Data;

    [Header("Current Position")]
    [SerializeField] private Vector3 curPos;


    [Header("Initialize Direction Moving For FlyingEye")]
    [SerializeField] protected Transform startPos;
    [SerializeField] protected Transform endPos;

    //protected float _target;
    protected float _current;
}
