using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSkullSkill : BaseSkill
{
    [SerializeField] private GameObject bulletObj;
    [SerializeField] private float speed;
    [SerializeField] private int maxBullet;
    [SerializeField] private GameObject pointBullet;
    [SerializeField] private Vector3 offset;

    private Demon demon;

    private void Awake()
    {
        demon = GetComponent<Demon>();
    }


    public override bool CanUseSkill()
    {
        throw new System.NotImplementedException();
    }

    public override void UseSkill()
    {
        StartCoroutine(SpawnBullet());
        
        IEnumerator SpawnBullet()
        {
            for (int i = 0; i < maxBullet; i++)
            {
                GameObject obj = GameObject.Find("FireSkullHolder");
                Transform holder = obj.transform.Find("Holder");
                Object_Pool objPool = holder.parent.GetComponentInChildren<Object_Pool>();

                Transform tf = objPool.GetTransformFromPool();
                Vector3 pointAttack = (i % 2 == 0) ? pointBullet.transform.position : pointBullet.transform.position + offset;
                if (tf != null)
                {
                    tf.transform.gameObject.SetActive(true);
                    tf.position = pointAttack;
                    tf.GetComponent<Rigidbody2D>().velocity = new Vector2(pointAttack.x, 0).normalized * speed * demon.facingDirection;
                }
                else
                {
                    GameObject objBullet = Instantiate(bulletObj.gameObject, pointAttack, Quaternion.identity, holder);
                    
                    objBullet.GetComponent<BulletSkill>().damage = damage;
                    objBullet.GetComponent<BulletSkill>().timeDestroy = timeDelay;

                    objBullet.GetComponent<Rigidbody2D>().velocity = new Vector2(pointAttack.x, 0).normalized * speed * demon.facingDirection;
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }


}
