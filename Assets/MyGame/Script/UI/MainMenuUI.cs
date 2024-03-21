using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button btnContinue;
    [SerializeField] private Image imgBtnContinue;
    [SerializeField] private TextMeshProUGUI textBtnContinue;
    [SerializeField] private Button btnNewGame;
    //[SerializeField] private bool isPlayed;

    private void Start()
    {
        Init();
        CheckStatusButtonContinue();

    }

    //private void Update()
    //{
    //    CheckStatusButtonContinue();
    //}

    private void Init()
    {
        btnContinue.onClick.AddListener(ButtonContinue);
        btnNewGame.onClick.AddListener(ButtonNewGame);
    }

    public void ButtonNewGame()
    {
        DataManager.GetInstance().dataPlayerSO.Reset();

        var curScene = DataManager.GetInstance().dataPlayerSO.curScene;
        LoadSceneManagement.LoadScene(curScene);
    }

    public void ButtonContinue()
    {
        DataManager.GetInstance().LoadData();

        var curScene = DataManager.GetInstance().dataPlayerSO.curScene;
        LoadSceneManagement.LoadScene(curScene);
    }

    public void CheckStatusButtonContinue()
    {
        var isPlayed = DataManager.GetInstance().HaveEntranceGameFirst();
        if (isPlayed)
        {
            imgBtnContinue.color = new Color(1, 1, 1, 1);
            textBtnContinue.color = new Color(1, 1, 1, .65f);
            btnContinue.interactable = true;
        }
        else
        {
            imgBtnContinue.color = new Color(.5f, .5f, .5f, 1);
            textBtnContinue.color = new Color(0, 0, 0, 1f);
            btnContinue.interactable = false;
        }
    }

    public void mouseon()
    {
        Debug.Log("on");
    }

    public void mouseoff()
    {
        Debug.Log("off");
    }


}
