using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicTrap : Trap
{
    [Header("List Bullet")]
    [SerializeField] private List<int> bulletSideLeft;
    [SerializeField] private List<int> bulletSideRight;

    [Header("Properties Bullet")]
    [SerializeField] private Vector3 offset;

    [SerializeField] private int minBullet;
    [SerializeField] private int maxBullet;

    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;

    private Object_Pool objPool;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        objPool = GetComponent<Object_Pool>();
    }
    private void Start()
    {
        data.curTime = Time.time;
    }

    private void Update()
    {
        AttackAnimation();
    }

    public void AttackAnimation()
    {
        if (Time.time >= (data.curTime + data.timeDelay))
        {
            anim.SetBool("Attack", true);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1)
            {
                data.curTime = Time.time;
                anim.SetBool("Attack", false);
            }
        }
    }

    public void SpawnBullet()
    {
        Clear();

        float angle = 0;
        int halfBullet = maxBullet / 2;
        Debug.Log("halfbullet : " + halfBullet);
        for (int j = 0; j < halfBullet; j++)
        {
            Debug.Log("j");
            while (true)
            {
                int randomAngle = UnityEngine.Random.Range(0, 75);
                Debug.Log("angle : " + randomAngle);
                if (!bulletSideLeft.Contains(randomAngle))
                {
                    float rad = randomAngle * Mathf.Deg2Rad;

                    float x = transform.position.x + Mathf.Sin(rad);
                    float y = transform.position.y + Mathf.Cos(rad);

                    Vector3 newVector = new Vector3(x, y, 0);

                    float speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
                    Vector3 direction = (newVector - transform.position).normalized * speed;

                    ToxicBullet bullet = objPool.GetTransformFromPool().GetComponent<ToxicBullet>();
                    bullet.gameObject.SetActive(true);

                    bullet.transform.position = transform.position + offset;
                    bullet.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                    break;
                }
            }

        }

        for (int k = halfBullet; halfBullet < maxBullet; halfBullet++)
        {
            while (true)
            {
                int randomAngle = UnityEngine.Random.Range(280, 355);
                if (!bulletSideLeft.Contains(randomAngle))
                {
                    float rad = randomAngle * Mathf.Deg2Rad;

                    float x = transform.position.x + Mathf.Sin(rad);
                    float y = transform.position.y + Mathf.Cos(rad);

                    Vector3 newVector = new Vector3(x, y, 0);

                    float speed = UnityEngine.Random.Range(minSpeed, maxSpeed);
                    Vector3 direction = (newVector - transform.position).normalized * speed;

                    ToxicBullet bullet = objPool.GetTransformFromPool().GetComponent<ToxicBullet>();
                    bullet.gameObject.SetActive(true);

                    bullet.transform.position = transform.position + offset;
                    bullet.rgbody2D.velocity = new Vector2(direction.x, direction.y);
                    break;
                }
            }
        }
    }

    public void Clear()
    {
        bulletSideLeft.Clear();
        bulletSideRight.Clear();
    }
}
