using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Shop UI")]
    [SerializeField] private Button btnShop;
    [SerializeField] public bool _isOpenShopUI;

    [SerializeField] private Button btnPause;
    [SerializeField] private bool _isOpenPauseUI;
    [SerializeField] private Slider volUI;

    [Header("Pause UI")]
    [SerializeField] private Button btnResume;
    [SerializeField] private Button btnReturnMenu;

    [Header("EndUI")]
    [SerializeField] private Button btnPlayAgain;
    [SerializeField] private Button btnQuit;

    [SerializeField] public bool _isShowTabUI;


    private void Start()
    {
        Init();

        ButtonFunction();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && !_isOpenShopUI)
        {
            var pauseUI = UIController.GetInstance().uiManager.GetPauseUI();
            var curTimeScale = Player.GetInstance().GetTimeScale();
            

            _isOpenPauseUI = false ? _isOpenPauseUI : !_isOpenPauseUI;
            _isShowTabUI = _isOpenPauseUI;

            float timeScale = _isOpenPauseUI ? 0 : curTimeScale;
            Time.timeScale = timeScale;

            pauseUI.transform.SetAsLastSibling();
            pauseUI.SetActive(_isOpenPauseUI);

        }

        if (Input.GetKeyDown(KeyCode.Tab) && !_isOpenPauseUI)
        {
            var shopUI = UIController.GetInstance().uiManager.GetShopUI();
            

            _isOpenShopUI = false ? _isOpenShopUI : !_isOpenShopUI;
            _isShowTabUI = _isOpenShopUI;

            shopUI.transform.SetAsLastSibling();
            shopUI.SetActive(_isOpenShopUI);

        }
    }

    private void Init()
    {
        var shopUI = UIController.GetInstance().uiManager.GetShopUI();
        var pauseUI = UIController.GetInstance().uiManager.GetPauseUI();
        var endUI = UIController.GetInstance().uiManager.GetEndUI();

        shopUI.SetActive(false);
        pauseUI.SetActive(false);
        endUI.SetActive(false);

        if (PlayerPrefs.HasKey("Volume"))
        {
            var vol = PlayerPrefs.GetFloat("Volume");
            volUI.value = vol;
        }
    }

    private void ButtonFunction()
    {

        btnResume.onClick.AddListener(ButtonResume);
        btnReturnMenu.onClick.AddListener(ButtonReturnMenu);
        btnPlayAgain.onClick.AddListener(ButtonPlayAgain);
        btnQuit.onClick.AddListener(ButtonQuit);

        volUI.onValueChanged.AddListener(OnVolChanged);
    }

    public void OnVolChanged(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        //Debug.Log("Volume " + value);
    }

    public void ButtonPlayAgain()
    {
        Time.timeScale = 1;
        DataManager.GetInstance().dataPlayerSO.curHealth = 100;
        DataManager.GetInstance().dataPlayerSO.curMana = 100;

        var curScene = DataManager.GetInstance().dataPlayerSO.curScene;

        var aSrcBackground = AudioController.GetInstance().manager.GetAudioSourceBackground();
        var aClipGame = AudioController.GetInstance().manager.GetAudioBGame();

        LoadSceneManagement.LoadScene(curScene, aClipGame, aSrcBackground);
        SoundClick();
    }
    public void ButtonQuit()
    {
        DataManager.GetInstance().SaveGame();

        var aSrcBackground = AudioController.GetInstance().manager.GetAudioSourceBackground();
        var aClipMenu = AudioController.GetInstance().manager.GetAudioBMenu();

        LoadSceneManagement.LoadScene("MainMenu", aClipMenu, aSrcBackground);
        SoundClick();
    }


    public void ButtonResume()
    {
        var pauseUI = UIController.GetInstance().uiManager.GetPauseUI();
        var curTimeDelta = Player.GetInstance().GetTimeScale();
        _isOpenPauseUI = false;
        _isShowTabUI = _isOpenPauseUI;

        pauseUI.SetActive(false);
        Time.timeScale = curTimeDelta;
        SoundClick();
    }

    public void ButtonReturnMenu()
    {
        DataManager.GetInstance().SaveGame();

        var aSrcBackground = AudioController.GetInstance().manager.GetAudioSourceBackground();
        var aClipMenu = AudioController.GetInstance().manager.GetAudioBMenu();

        LoadSceneManagement.LoadScene("MainMenu", aClipMenu, aSrcBackground);
        SoundClick();

    }

    private static void SoundClick()
    {
        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aClipClick = AudioController.GetInstance().manager.GetAudioClick();

        AudioController.GetInstance().StartMusic(aClipClick, aSrc);
    }
}
