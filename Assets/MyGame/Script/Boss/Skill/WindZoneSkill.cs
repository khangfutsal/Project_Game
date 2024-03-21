using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZoneSkill : BaseSkill
{
    [SerializeField] private Player player;
    [SerializeField] private float strength;
    [SerializeField] private Vector2 direciton;
    private void Awake()
    {
        player = GameObject.Find("BonzePlayer").GetComponent<Player>();
    }

    public override bool CanUseSkill()
    {
        throw new System.NotImplementedException();
    }

    private void FixedUpdate()
    {
        if (_useSkill)
        {
            player.RgBody2D.AddForce(direciton);
        }
    }

    public override void UseSkill()
    {
        float random = UnityEngine.Random.Range(-1, 1);
        float sign = Mathf.Sign(random);
        if (sign > 0)
        {
            direciton = new Vector2(1 * strength, 0);
        }
        else
        {
            direciton = new Vector2(-1 * strength, 0);
        }
        StartCoroutine(UseWind());

        IEnumerator UseWind()
        {
            _useSkill = true;
            yield return new WaitForSeconds(duration);
            _useSkill = false;
        }
    }




}
