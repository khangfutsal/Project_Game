using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public static Enemy GetEnemyType<T>() where T : Enemy
    {
        object a = typeof(T);
        if (a is Enemy)
        {
            return a as Enemy;
        }   

        return null;
    }
}
