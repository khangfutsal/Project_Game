using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Object_Pool : MonoBehaviour
{
    [SerializeField] private List<Transform> listPoolTf;

    [SerializeField] private GameObject obj;

    [SerializeField] private Transform holderTf;
    private void Awake()
    {
        holderTf = transform.parent.Find("Holder");
    }
    private void Start()
    {
        Initialize();
    }



    public void Initialize()
    {
        if (holderTf == null) return;
        listPoolTf.Clear();

        foreach (var bulletTf in holderTf)
        {
            listPoolTf.Add((Transform)bulletTf);
        }
    }

    public GameObject SpawnObj()
    {
        GameObject gameObj = Instantiate(obj, transform.position, Quaternion.identity,holderTf);
        return gameObj;
    }

    public Transform GetTransformFromPool()
    {
        Initialize();
        if (obj != null)
        {
            if (listPoolTf.Count == 0 || listPoolTf.TrueForAll(obj => obj.gameObject.activeSelf))
            {

                GameObject gameObj = SpawnObj();
                gameObj.SetActive(false);
                listPoolTf.Add(gameObj.transform);
            }
        }

        foreach (var poolTf in listPoolTf)
        {
            if (!poolTf.gameObject.activeSelf) return poolTf;
        }
        return null;
    }
}
