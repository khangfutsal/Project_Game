using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim;
    public EnemyData enemyData;
    public Transform playerTf;

    protected virtual void Awake()
    {
        
    }
}
