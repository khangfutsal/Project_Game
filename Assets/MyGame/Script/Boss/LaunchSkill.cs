using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaunchSkill : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float timeDestroy;
    [SerializeField] public List<GameObject> carpets;

    [SerializeField] private Sprite[] Sprites;
    [SerializeField] private PolygonCollider2D[] Colliders;
    private UnityEvent myEvent;
    private int index = 0; 
    private SpriteRenderer sp;


    public void ActiveEvent(UnityEvent  _event)
    {
        myEvent = _event;
    }


    private void Awake()
    {
        sp = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        sp.sprite = Sprites[index];
        Colliders[0].enabled = true;
    }



    void LateUpdate()
    {

        UpdateCollider();

    }

    void UpdateCollider()
    {
        if (sp.sprite.name == Sprites[index].name) return;

        Colliders[index].enabled = false;

        index++;
        if (index > Sprites.Length - 1)
        {
            index = 0;
        }
        Colliders[index].enabled = true;



    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            IDmgable Idmg = collision.GetComponent<IDmgable>();
            if (Idmg != null)
            {
                myEvent?.Invoke();
                Idmg.TakeDamage(damage, transform);
            }
        }

    }


    private void OnEnable()
    {
        foreach (var obj in carpets)
        {
            obj.GetComponent<CarpetSkill>().isActive = false;
        }
        Invoke("InActive", timeDestroy);
        foreach(var obj in carpets)
        {
            if(Vector3.Distance(obj.transform.position,transform.position) < 3f)
            {
                obj.SetActive(true);
                obj.GetComponent<CarpetSkill>().damage = damage;
                obj.GetComponent<CarpetSkill>().ActiveEvent(myEvent);
            }
        }
    }


    public void InActive()
    {

       
        transform.gameObject.SetActive(false);

    }
}
