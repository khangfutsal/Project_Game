using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_Pool : MonoBehaviour
{
    [SerializeField] private List<Transform> listBulletTf;
    [SerializeField] private Transform holderTf;
    private void Awake()
    {
        holderTf = transform.Find("Holder");
    }
    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        foreach(var bulletTf in holderTf)
        {
            listBulletTf.Add((Transform)bulletTf);
        }
    }

    public Transform GetBulletFromPool()
    {
        foreach(var bulletTf in listBulletTf)
        {
            if (!bulletTf.gameObject.activeInHierarchy) return bulletTf;
        }
        return null;
    }
}
