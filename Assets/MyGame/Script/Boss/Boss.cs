using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private List<BaseSkill> skills;
    public override void Chase()
    {
        Debug.Log("Chase");
    }
}