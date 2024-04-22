using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderSkill : MonoBehaviour
{
    [SerializeField] public float timeDestroy;
    [SerializeField] public Rigidbody2D rgbody2D;
    [SerializeField] private int maxBullet;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speedBullet;


    [SerializeField] private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rgbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rgbody2D.bodyType = RigidbodyType2D.Dynamic;
        Invoke("InActive", timeDestroy);
    }

    private void FixedUpdate()
    {
        transform.right = rgbody2D.velocity;
    }

    private void OnDisable()
    {
        anim.SetBool("Explode", false);

    }

    public void SpawnBullet()
    {
        GameObject obj = GameObject.Find("MultiShoot");
        float stepAngle = 90;
        float angle = 180 / maxBullet;
        for (int i = 0; i < maxBullet; i++)
        {
            
            float rad = stepAngle * Mathf.Deg2Rad;

            float x = transform.position.x + Mathf.Sin(rad);
            float y = transform.position.y + Mathf.Cos(rad);

            Vector3 newVector = new Vector3(x, y, 0);

            Vector3 direction = (newVector - transform.position).normalized * speedBullet;

            Transform holder = obj.transform.Find("Holder");
            Object_Pool objPool = holder.parent.GetComponentInChildren<Object_Pool>();

            Transform tf = objPool.GetTransformFromPool();
            if (tf != null)
            {
                tf.transform.gameObject.SetActive(true);
                tf.position = transform.position;

                BulletSkill bulletSkill = tf.transform.GetComponent<BulletSkill>();
                bulletSkill.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                bulletSkill.transform.right = bulletSkill.rgbody2D.velocity;

            }
            else
            {
                GameObject objBullet = Instantiate(bullet.gameObject, transform.position, Quaternion.identity, holder);
                BulletSkill bulletSkill = objBullet.transform.GetComponent<BulletSkill>();
                bulletSkill.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                bulletSkill.transform.right = bulletSkill.rgbody2D.velocity;
            }

            stepAngle += angle;
        }
    }

    public void InActive()
    {
        VFX_Controller.GetInstance().SpawnFireWorkVFX(transform);
        SpawnBullet();
        Destroy(gameObject);
        
    }
}
