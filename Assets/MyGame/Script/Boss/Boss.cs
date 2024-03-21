using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] public List<BaseSkill> skillsList;
    public override void Chase()
    {
        Debug.Log("Chase");
    }
}
