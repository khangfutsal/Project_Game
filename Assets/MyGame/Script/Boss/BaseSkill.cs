using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseSkill : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public string typeSkill;
    [SerializeField] public int phase;
    [Header("Properties Skills")]
    [SerializeField] public float curTime;
    [SerializeField] public float timeDelay;
    [SerializeField] public float damage;
    [SerializeField] public float duration;

    public UnityEvent OnHitEffect = new UnityEvent();

    [SerializeField] public bool _useSkill;
    
    public abstract void UseSkill();
    public abstract bool CanUseSkill();


}
