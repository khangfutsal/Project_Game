using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallBullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rgBody2D;
    [SerializeField] private float timeDestroy;
    [SerializeField] private float dmg;

    private void Awake()
    {
        rgBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void SetDirection(Transform tf)
    {
        transform.position = tf.position;
        transform.rotation = tf.rotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            transform.gameObject.SetActive(false);
            IDmgable Idmg = collision.GetComponent<IDmgable>();
            if(Idmg != null)
            {
                var aSrc = AudioController.GetInstance().manager.GetAudioSource();
                var aClipAttack = AudioController.GetInstance().manager.GetAudioAttack();
                AudioController.GetInstance().StartMusic(aClipAttack, aSrc);

                CameraShake.GetInstance().ShakeCamera(3, .5f, .1f);
                Idmg.TakeDamage(dmg, transform);
            }
        }

    }

    private void OnEnable()
    {
        Invoke("InActive", timeDestroy);
    }

    public void InActive()
    {
        transform.gameObject.SetActive(false);
    }


}
