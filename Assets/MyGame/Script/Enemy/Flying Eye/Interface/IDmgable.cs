using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDmgable 
{
    public float maxHealth { get; set; }
    public float health { get; set; }

    public void TakeDamage(float dmg,Transform tf = null);
    public void Die();
}
