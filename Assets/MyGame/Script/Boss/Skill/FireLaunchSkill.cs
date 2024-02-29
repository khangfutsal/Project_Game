using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireLaunchSkill : BaseSkill
{
    [SerializeField] private GameObject objFire;

    [SerializeField] private GameObject PointFire;



    public override bool CanUseSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSkill()
    {
        objFire.GetComponent<LaunchSkill>().timeDestroy = duration;
        objFire.GetComponent<LaunchSkill>().damage = damage;
        objFire.GetComponent<LaunchSkill>().ActiveEvent(OnHitEffect);

        var carpets = objFire.GetComponent<LaunchSkill>().carpets;
        foreach(var carpet in carpets)
        {
            carpet.GetComponent<CarpetSkill>().ActiveEvent(OnHitEffect);
        }
        objFire.SetActive(true);

        objFire.transform.position = PointFire.transform.position;
        objFire.transform.rotation = PointFire.transform.rotation;
    }


}
