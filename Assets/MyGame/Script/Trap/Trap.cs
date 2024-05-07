using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Trap : MonoBehaviour
{
    [SerializeField] public TrapPatern data;

    [SerializeField] public Animator anim;

}

public enum TrapType
{
    none,
    Lightning,
    Fire,
    Arrow
}


