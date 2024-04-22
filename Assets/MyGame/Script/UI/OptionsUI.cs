using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    [SerializeField] private GameObject optionsUI;
    [Header("Component Of Graphics")]
    [SerializeField] private Button btnGraphicsLow;
    [SerializeField] private Button btnGraphicsMed;
    [SerializeField] private Button btnGraphicsUltra;

    [SerializeField] private TextMeshProUGUI GraphicsLowText;
    [SerializeField] private TextMeshProUGUI GraphicsMedText;
    [SerializeField] private TextMeshProUGUI GraphicsUltraText;

    [Header("Component Of Texture Quality")]
    [SerializeField] private Button btnTextureLow;
    [SerializeField] private Button btnTextureMed;
    [SerializeField] private Button btnTextureHigh;

    [SerializeField] private TextMeshProUGUI TextureLowText;
    [SerializeField] private TextMeshProUGUI TextureMedText;
    [SerializeField] private TextMeshProUGUI TextureHighText;

    [Header("Component Of Vsync")]
    [SerializeField] private Button btnTurnVsync;
    [SerializeField] private TextMeshProUGUI vSyncText;

    [Header("Component Of Volume")]
    [SerializeField] private Slider sliderVol;

    [Header("Component Of Volume")]
    [SerializeField] private Button btnClose;
    [SerializeField] private GameObject mainMenuUI;

    private void Start()
    {
        Init();
        ButtonFunction();
    }

    public void Init()
    {

        if (PlayerPrefs.GetFloat("vSync") == 0)
        {
            QualitySettings.vSyncCount = 0;
            vSyncText.text = "Off";
            HighlightVsync();
        }
        else if (PlayerPrefs.GetFloat("vSync") == 1)
        {
            QualitySettings.vSyncCount = 1;
            vSyncText.text = "On";
            HighlightVsync();
        }

        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetFloat("Volume", 100);
        }
        else
        {
            var vol = PlayerPrefs.GetFloat("Volume");
            sliderVol.value = vol;
        }

        if (PlayerPrefs.GetFloat("TextureQuality") == 0)
        {
            HighlightTextureHigh();
        }
        else if (PlayerPrefs.GetFloat("TextureQuality") == 1)
        {
            HighlightTextureMed();
        }
        else if (PlayerPrefs.GetFloat("TextureQuality") == 2)
        {
            HighlighTextureLow();
        }

        if (PlayerPrefs.GetFloat("Graphics") == 1)
        {
            HighlightGraphicsLow();
        }
        else if (PlayerPrefs.GetFloat("Graphics") == 2)
        {
            HighlightGraphicsMed();
        }
        else if (PlayerPrefs.GetFloat("Graphics") == 5)
        {
            HighlightGraphicsUltra();
        }


    }

    private void ButtonFunction()
    {
        btnTextureLow.onClick.AddListener(ButtonTextureLow);
        btnTextureMed.onClick.AddListener(ButtonTextureMed);
        btnTextureHigh.onClick.AddListener(ButtonTextureHigh);

        btnGraphicsLow.onClick.AddListener(ButtonGraphicsLow);
        btnGraphicsMed.onClick.AddListener(ButtonGraphicsMed);
        btnGraphicsUltra.onClick.AddListener(ButtonGraphicsUltra);

        btnTurnVsync.onClick.AddListener(ButtonTurnVsync);

        btnClose.onClick.AddListener(ButtonClose);

        sliderVol.onValueChanged.AddListener(OnVolChanged);
    }
    #region Texture Function
    private void ButtonTextureLow()
    {
        SoundClick();

        QualitySettings.masterTextureLimit = 2;
        PlayerPrefs.SetFloat("TextureQuality", 2);

        HighlighTextureLow();

    }

    private void HighlighTextureLow()
    {
        TextureLowText.color = new Color(1, 0.7802028f, 0.1803921f);
        TextureMedText.color = new Color(1, 1, 1);
        TextureHighText.color = new Color(1, 1, 1);
    }

    private void ButtonTextureMed()
    {
        SoundClick();

        QualitySettings.masterTextureLimit = 1;
        PlayerPrefs.SetFloat("TextureQuality", 1);

        HighlightTextureMed();
    }

    private void HighlightTextureMed()
    {
        TextureLowText.color = new Color(1, 1, 1);
        TextureMedText.color = new Color(1, 0.7802028f, 0.1803921f);
        TextureHighText.color = new Color(1, 1, 1);
    }

    private void ButtonTextureHigh()
    {
        SoundClick();

        QualitySettings.masterTextureLimit = 0;
        PlayerPrefs.SetFloat("TextureQuality", 0);

        HighlightTextureHigh();
    }

    private void HighlightTextureHigh()
    {
        TextureLowText.color = new Color(1, 1, 1);
        TextureMedText.color = new Color(1, 1, 1);
        TextureHighText.color = new Color(1, 0.7802028f, 0.1803921f);
    }
    #endregion

    #region Graphics Function
    public void ButtonGraphicsLow()
    {
        SoundClick();

        QualitySettings.SetQualityLevel(1);
        PlayerPrefs.SetFloat("Graphics", 1);

        HighlightGraphicsLow();
    }

    private void HighlightGraphicsLow()
    {
        GraphicsLowText.color = new Color(1, 0.7802028f, 0.1803921f);
        GraphicsMedText.color = new Color(1, 1, 1);
        GraphicsUltraText.color = new Color(1, 1, 1);
    }

    public void ButtonGraphicsMed()
    {
        SoundClick();

        QualitySettings.SetQualityLevel(2);
        PlayerPrefs.SetFloat("Graphics", 2);

        HighlightGraphicsMed();
    }

    private void HighlightGraphicsMed()
    {
        GraphicsLowText.color = new Color(1, 1, 1);
        GraphicsMedText.color = new Color(1, 0.7802028f, 0.1803921f);
        GraphicsUltraText.color = new Color(1, 1, 1);
    }

    public void ButtonGraphicsUltra()
    {
        SoundClick();

        QualitySettings.SetQualityLevel(5);
        PlayerPrefs.SetFloat("Graphics", 5);

        HighlightGraphicsUltra();
    }

    private void HighlightGraphicsUltra()
    {
        GraphicsLowText.color = new Color(1, 1, 1);
        GraphicsMedText.color = new Color(1, 1, 1);
        GraphicsUltraText.color = new Color(1, 0.7802028f, 0.1803921f);
    }
    #endregion

    #region Vsync Function
    public void ButtonTurnVsync()
    {
        SoundClick();

        if (QualitySettings.vSyncCount == 0)
        {
            PlayerPrefs.SetFloat("vSync", 1);
            QualitySettings.vSyncCount = 1;

            vSyncText.text = "On";

            Debug.Log("0");


        }
        else if (QualitySettings.vSyncCount == 1)
        {
            PlayerPrefs.SetFloat("vSync", 0);
            QualitySettings.vSyncCount = 0;

            vSyncText.text = "Off";

            Debug.Log("1q");
        }
        HighlightVsync();
    }

    public void HighlightVsync()
    {
        if (vSyncText.text == "On")
        {
            vSyncText.color = new Color(1, 0.7802028f, 0.1803921f);
        }
        else vSyncText.color = new Color(1, 1, 1);
    }
    #endregion

    #region Close Function
    public void ButtonClose()
    {
        SoundClick();

        mainMenuUI.SetActive(true);
        optionsUI.SetActive(false);

        mainMenuUI.transform.SetAsLastSibling();
    }
    #endregion

    public void OnVolChanged(float value)
    {
        PlayerPrefs.SetFloat("Volume", value);
        //Debug.Log("Volume " + value);
    }

    private static void SoundClick()
    {
        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aClipClick = AudioController.GetInstance().manager.GetAudioClick();

        AudioController.GetInstance().StartMusic(aClipClick, aSrc);
    }


}
