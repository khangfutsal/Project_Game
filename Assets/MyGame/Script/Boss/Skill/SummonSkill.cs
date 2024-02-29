using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSkill : BaseSkill
{
    [SerializeField] private List<EnemyPattern> enemies;

    private void Update()
    {
        //CanUseSkill();
    }
    public override void UseSkill()
    {
        Debug.Log("use summon");
        curTime = Time.time;
        _useSkill = true;


        foreach (var enemy in enemies)
        {
            float xPos = UnityEngine.Random.Range(13.5f, 28.32f);
            Vector3 pos = new Vector3(xPos, 0, 0);
            GameObject enemyObj = Instantiate(enemy.enemyObj, pos, Quaternion.identity);
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