using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceSkill1 : BaseSkill
{
    [SerializeField] private int angelDesired;
    [SerializeField] private int maxBullet;
    [SerializeField] private int timesShoot;
    [SerializeField] private float offset;
    [SerializeField] private float speed;
    [SerializeField] private Transform pointBullet;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Demon demon;

    private void Awake()
    {
        demon = GetComponent<Demon>();

    }





    public override void UseSkill()
    {
        StartCoroutine(Shoot());
        
        IEnumerator Shoot()
        {
            float angle;
            float stepAngle;
            int facingDemon = demon.facingDirection;

            StartCoroutine(SpawnSkill(facingDemon));
           
            IEnumerator SpawnSkill(int facingDemon)
            {
                GameObject obj = GameObject.Find("IceSkill1");

                if (facingDemon == 1)
                {
                    for (int i = 0; i < timesShoot; i++)
                    {
                        stepAngle = 90;
                        if (i % 2 == 0)
                        {
                            angle = angelDesired / maxBullet;
                        }
                        else
                        {
                            angle = angelDesired / maxBullet - offset;
                        }

                        for (int j = 0; j < maxBullet; j++)
                        {
                            float rad = stepAngle * Mathf.Deg2Rad;

                            float x = pointBullet.position.x + Mathf.Sin(rad);
                            float y = pointBullet.position.y + Mathf.Cos(rad);

                            Vector3 newVector = new Vector3(x, y, 0);

                            Vector3 direction = (newVector - pointBullet.position).normalized * speed;

                            Transform holder = obj.transform.Find("Holder");
                            Object_Pool objPool = holder.parent.GetComponentInChildren<Object_Pool>();

                            Transform tf = objPool.GetTransformFromPool();
                            if (tf != null)
                            {
                                tf.transform.gameObject.SetActive(true);
                                tf.position = pointBullet.position;

                                BulletSkill bulletSkill = tf.transform.GetComponent<BulletSkill>();
                                bulletSkill.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                                bulletSkill.transform.right = bulletSkill.rgbody2D.velocity;

                            }
                            else
                            {
                                GameObject objBullet = Instantiate(bullet.gameObject, pointBullet.position, Quaternion.identity, holder);
                                BulletSkill bulletSkill = objBullet.transform.GetComponent<BulletSkill>();
                                bulletSkill.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                                bulletSkill.transform.right = bulletSkill.rgbody2D.velocity;
                            }

                            stepAngle += angle;
                        }
                        yield return new WaitForSeconds(2);
                    }
                }
                else
                {
                    for (int i = 0; i < timesShoot; i++)
                    {
                        stepAngle = 270;
                        if (i % 2 == 0)
                        {
                            angle = angelDesired / maxBullet;
                        }
                        else
                        {
                            angle = angelDesired / maxBullet - offset;
                        }

                        for (int j = 0; j < maxBullet; j++)
                        {
                            float rad = stepAngle * Mathf.Deg2Rad;

                            float x = pointBullet.position.x + Mathf.Sin(rad);
                            float y = pointBullet.position.y + Mathf.Cos(rad);

                            Vector3 newVector = new Vector3(x, y, 0);

                            Vector3 direction = (newVector - pointBullet.position).normalized * speed;

                            Transform holder = obj.transform.Find("Holder");
                            Object_Pool objPool = holder.parent.GetComponentInChildren<Object_Pool>();

                            Transform tf = objPool.GetTransformFromPool();
                            if (tf != null)
                            {
                                tf.transform.gameObject.SetActive(true);
                                tf.position = pointBullet.position;
                                BulletSkill bulletSkill = tf.transform.GetComponent<BulletSkill>();
                                bulletSkill.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                                bulletSkill.transform.right = bulletSkill.rgbody2D.velocity;

                            }
                            else
                            {
                                GameObject objBullet = Instantiate(bullet.gameObject, pointBullet.position, Quaternion.identity, holder);
                                BulletSkill bulletSkill = objBullet.transform.GetComponent<BulletSkill>();
                                bulletSkill.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                                bulletSkill.transform.right = bulletSkill.rgbody2D.velocity;
                            }

                            stepAngle -= angle;
                        }
                        yield return new WaitForSeconds(2);
                    }
                }
                
               
            }

            yield return null;

        }


    }

    public override bool CanUseSkill()
    {
        throw new System.NotImplementedException();
    }


}



