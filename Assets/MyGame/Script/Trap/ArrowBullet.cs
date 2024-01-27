using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBullet : MonoBehaviour
{
    [SerializeField] private float speed;

    private ArrowTrap arrowTrap;
    private float dmg;
    private void Awake()
    {
        arrowTrap = transform.parent.parent.GetComponentInChildren<ArrowTrap>();
    }

    void Update()
    {
        if (!transform.gameObject.activeSelf) return;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InActive();
            IDmgable damageable = collision.GetComponent<IDmgable>();
            dmg = arrowTrap.data.dmg;
            if (damageable != null)
            {
                damageable.TakeDamage(dmg);
            }
        }

    }
    private void OnEnable()
    {
        Invoke("InActive", 2f);
    }
    public void SetDirection(Transform directionTf)
    {
        transform.position = directionTf.position;
    }
    public void InActive() { transform.gameObject.SetActive(false); }
}