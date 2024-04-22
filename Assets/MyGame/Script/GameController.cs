using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private static GameController _ins;

    public static GameController GetInstance() => _ins;
    [SerializeField] public GameManager gameManager;

    private void Awake()
    {
        _ins = this;
    }

    private void Start()
    {
        Init();
        DontDestroyOnLoad(this);
    }

    private void Init()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        Debug.Log(currentSceneName);
        DataManager.GetInstance().dataPlayerSO.curScene = currentSceneName;
    }



}
