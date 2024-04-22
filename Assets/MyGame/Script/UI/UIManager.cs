using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Shop UI")]
    [SerializeField] private GameObject shopUI;

    [Header("Pause UI")]
    [SerializeField] private GameObject pauseUI;

    [Header("End UI")]
    [SerializeField] private GameObject endUI;
    [Header("GuideSkill UI")]
    [SerializeField] private GameObject guideSkillUI;
    [Header("Game UI")]
    [SerializeField] private GameUI gameUI;


    [Header("Wave UI")]
    [SerializeField] private TextMeshProUGUI titleWave;

    #region Get Function
    public GameObject GetShopUI() => shopUI;
    public GameObject GetPauseUI() => pauseUI;
    public GameObject GetEndUI() => endUI;
    public GameObject GetGuideSKillUI() => guideSkillUI;
    public GameUI GetGameUI() => gameUI;


    public TextMeshProUGUI GetTitleWave() => titleWave;
    #endregion
}
