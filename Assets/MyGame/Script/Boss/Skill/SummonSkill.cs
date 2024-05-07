using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSkill : BaseSkill
{
    [SerializeField] private List<EnemyPattern> enemies;
    [SerializeField] private float xMinBound;
    [SerializeField] private float xMaxBound;

    private void Update()
    {
        //CanUseSkill();
    }
    public override void UseSkill()
    {
        Debug.Log("USe summon");
        curTime = Time.time;
        _useSkill = true;


        foreach (var enemy in enemies)
        {
            float xPos = UnityEngine.Random.Range(xMinBound, xMaxBound);
            Vector3 pos = new Vector3(xPos, 0, 0);
            GameObject enemyObj = Instantiate(enemy.enemyObj, pos, Quaternion.identity);
        }
    }

    public void UselessSkill()
    {
        GameObject enemies = GameObject.Find("Enemy");
        if (enemies != null)
        {
            foreach (Transform enemy in enemies.transform) 
            {
                Destroy(enemy.gameObject);
            }
        }
    }

    public override bool CanUseSkill()
    {
        if (_useSkill)
        {
            if (Time.time >= curTime + timeDelay)
            {
                _useSkill = false;
                return true;
            }
        }
        return false;
    }


}