using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class TrapPatern
{
    [SerializeField] public GameObject trapObj;
    [SerializeField] public GameObject spawnPos;

    [SerializeField] public float timeDelay;
    [SerializeField] public float curTime;

    [SerializeField] public float dmg;

}
