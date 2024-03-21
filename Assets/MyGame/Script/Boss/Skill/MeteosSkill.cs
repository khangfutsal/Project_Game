using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteosSkill : BaseSkill
{
    [SerializeField] private GameObject obj;
    public override bool CanUseSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSkill()
    {
        StartCoroutine(SpawnSkill());
        IEnumerator SpawnSkill()
        {
            obj.SetActive(true);
            obj.GetComponent<VFXCollision>().damage = damage;
            yield return new WaitForSeconds(duration);
            obj.SetActive(false);

        }
    }
}
