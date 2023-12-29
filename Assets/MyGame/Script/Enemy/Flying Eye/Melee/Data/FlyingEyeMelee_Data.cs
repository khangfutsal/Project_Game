using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FlyingEyeMelee_Data : FlyingEyeData
{
    [Header("Check Variables")]
    public float groundCheckRadius = .3f;
    public LayerMask whatIsGround;
}
