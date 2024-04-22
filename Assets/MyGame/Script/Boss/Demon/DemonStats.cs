using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonStats : MonoBehaviour, IDmgable
{
    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }

    private Demon demon;

    private void Awake()
    {
        demon = GetComponent<Demon>();
    }

    private void Start()
    {
        health = maxHealth;

        BossHealthBar.GetInstance().maxValue = maxHealth;
    }

    public void Die()
    {
        transform.tag = "Untagged";
        demon.SetBool_IsDeath(true);
    }

    public void TakeDamage(float dmg, Transform tf = null)
    {
        health -= dmg;

        BossHealthBar.GetInstance().ChangeValueHealth(health);

        if (health <= 0) { Die(); health = 0; }


    }
}
