using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStats : MonoBehaviour, IDmgable
{
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }

    private void Start()
    {
        health = maxHealth;
    }

    public void Die()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(float dmg, Transform tf = null)
    {
        throw new System.NotImplementedException();
    }
}
