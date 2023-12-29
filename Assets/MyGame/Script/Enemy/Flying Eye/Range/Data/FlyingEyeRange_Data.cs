using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class FlyingEyeRange_Data : FlyingEyeData
{
    [Range(0, 5)]
    public float groundCheckRadius;

    public LayerMask whatIsPlayer;

}
