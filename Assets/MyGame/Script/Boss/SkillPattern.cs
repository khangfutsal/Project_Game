using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SkillPattern
{
    [SerializeField] public GameObject skillObj;

    [SerializeField] public Transform spawnTf;

    [SerializeField] public float delay;
    [SerializeField] public float curTime;
}
