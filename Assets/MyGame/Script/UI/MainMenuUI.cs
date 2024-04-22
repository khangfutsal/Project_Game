using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuUI;

    [Space(5)]
    [SerializeField] private Button btnContinue;
    [SerializeField] private Image imgBtnContinue;
    [SerializeField] private TextMeshProUGUI textBtnContinue;

    [Space(5)]
    [SerializeField] private Button btnNewGame;

    [Space(5)]
    [SerializeField] private Button btnOptions;
    [SerializeField] private GameObject optionsUI;

    [Space(5)]
    [SerializeField] private Button btnQuit;
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
        mainMenuUI.SetActive(true);
        optionsUI.SetActive(false);

        btnContinue.onClick.AddListener(ButtonContinue);
        btnNewGame.onClick.AddListener(ButtonNewGame);
        btnOptions.onClick.AddListener(ButtonOptions);
        btnQuit.onClick.AddListener(ButtonQuit);
    }

    public void ButtonNewGame()
    {
        SoundClick();

        DataManager.GetInstance().dataPlayerSO.Reset();

        var curScene = DataManager.GetInstance().dataPlayerSO.curScene;

        var aSrcBackground = AudioController.GetInstance().manager.GetAudioSourceBackground();
        var aClipGame = AudioController.GetInstance().manager.GetAudioBGame();

        LoadSceneManagement.LoadScene(curScene, aClipGame, aSrcBackground);
    }

    public void ButtonContinue()
    {
        SoundClick();

        DataManager.GetInstance().LoadData();

        var curScene = DataManager.GetInstance().dataPlayerSO.curScene;

        var aSrcBackground = AudioController.GetInstance().manager.GetAudioSourceBackground();
        var aClipGame = AudioController.GetInstance().manager.GetAudioBGame();

        LoadSceneManagement.LoadScene("Chap2", aClipGame, aSrcBackground);
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

    public void ButtonQuit()
    {
        SoundClick();

        Application.Quit();
    }

    private static void SoundClick()
    {
        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aClipClick = AudioController.GetInstance().manager.GetAudioClick();

        AudioController.GetInstance().StartMusic(aClipClick, aSrc);
    }

    public void ButtonOptions()
    {
        SoundClick();

        optionsUI.SetActive(true);
        mainMenuUI.SetActive(false);

        transform.SetAsLastSibling();
    }
    


}
