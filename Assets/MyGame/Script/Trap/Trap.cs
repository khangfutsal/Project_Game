using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class Trap : MonoBehaviour
{
    [SerializeField] protected GameObject trapObj;
    [SerializeField] protected Transform spawnPos;

    [SerializeField] protected float timeDelay;
    [SerializeField] protected float curTime;

    [SerializeField] public float dmg;
    [SerializeField] protected Animator anim;

    private void Start()
    {
        Spawn();
    }

    public void Spawn()
    {
        if (spawnPos == null && trapObj == null) return;
        GameObject obj = Instantiate(trapObj, spawnPos.position, Quaternion.identity);
    }
}

public enum TrapType
{
    none,
    Lightning,
    Fire,
    Arrow
}
