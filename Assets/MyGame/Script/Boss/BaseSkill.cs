using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkill : MonoBehaviour
{
    [SerializeField] protected GameObject skillObj;

    [SerializeField] protected Transform spawnTf;

    [SerializeField] protected float delay;
    [SerializeField] protected float curTime;

}
