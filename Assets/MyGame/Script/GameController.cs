using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private static GameController _ins;

    public static GameController GetInstance() => _ins;
    private void Awake()
    {
        _ins = this;
    }

    public Player player;

   
}
