using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapController : MonoBehaviour
{
    [SerializeField] public TilemapManager manager;

    private static TilemapController _ins;

    public static TilemapController GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;
    }

    private void Start()
    {
        manager.ShowDefaultMap();
    }
}
