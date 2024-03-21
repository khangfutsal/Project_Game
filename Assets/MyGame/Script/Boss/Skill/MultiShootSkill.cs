using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShootSkill : BaseSkill
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private GameObject pointAttack;


    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            UseSkill();

        }
    }
    public override bool CanUseSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSkill()
    {
        GameObject obj = GameObject.Find("MultiShoot");
        float xVelocity = 2;
        float yVelocity = 15;
        for (int i = 0; i < 2; i++)
        {

            if (i % 2 == 0)
            {

                GameObject bullet = Instantiate(bulletObj, pointAttack.transform.position, Quaternion.identity);

                Rigidbody2D rg = bullet.GetComponent<Rigidbody2D>();

                rg.velocity = new Vector2(xVelocity, yVelocity);
            }
            else
            {

                GameObject bullet = Instantiate(bulletObj, pointAttack.transform.position, Quaternion.identity);

                Rigidbody2D rg = bullet.GetComponent<Rigidbody2D>();

                rg.velocity = new Vector2(-xVelocity, yVelocity);
            }



        }
    }
}
