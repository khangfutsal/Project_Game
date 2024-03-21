using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DataPlayerSO", menuName = "ScriptableObject/DataPlayerSO")]
public class DataPlayerSO : ScriptableObject
{
    [Header("Properties Of Player")]
    public float curHealth;
    public float curMana;

    public int curDamage;
    [Header("Gem")]
    public int curCoin;
    public int curCrystal;
    [Header("Buff")]
    public float percentRegenerationHealth;
    public float timeRegenerationHealth;

    public float statsRegenerationMana;
    public float timeRegenerationMana;

    [Header("Skills")]
    public float statusFireballSkill;
    public float statusEarthquakeSkill;
    public float statusDefenseSkill;
    [Header("Check Points")]
    public string curScene;
    [Header("Card UI")]
    public List<CardInfo> listCards;
    public List<CardInfo> curCardsUI;

    public void Reset()
    {
        Debug.Log("Reset");

        curHealth = 100;
        curMana = 100;
        curCoin = 0;
        curCrystal = 0;
        curDamage = 3;
        percentRegenerationHealth = 0;
        timeRegenerationHealth = 0;
        statsRegenerationMana = 0;
        timeRegenerationMana = 0;
        statusDefenseSkill = 0;
        statusEarthquakeSkill = 0;
        statusDefenseSkill = 0;
        curScene = "Chap1";

        GetCardScriptableObj();
    }

    private void GetCardScriptableObj()
    {
        curCardsUI.Clear();
        listCards.Clear();

        GetDataCards();
        InitCardDefault();


        void GetDataCards()
        {
            ScriptableObject[] asset = Resources.LoadAll<ScriptableObject>("Cards");
            if (asset != null)
            {
                foreach (var i in asset)
                {
                    listCards.Add((CardInfo)i);
                }
            }
        }

        void InitCardDefault()
        {
            foreach (var i in listCards)
            {
                if (i.ToString().Contains("LV1"))
                {
                    curCardsUI.Add(i);
                }
            }
        }
    }


}
[Serializable]
public class DataPlayerPattern
{
    [Header("Properties Of Player")]
    public float curHealth;
    public float curMana;

    public int curDamage;
    [Header("Gem")]
    public int curCoin;
    public int curCrystal;
    [Header("Buff")]
    public float percentRegenerationHealth;
    public float timeRegenerationHealth;

    public float statsRegenerationMana;
    public float timeRegenerationMana;

    [Header("Skills")]
    public float statusFireballSkill;
    public float statusEarthquakeSkill;
    public float statusDefenseSkill;
    [Header("Check Points")]
    public string curScene;
    [Header("Card UI")]
    public List<CardInfo> listCards;
    public List<CardInfo> curCardsUI;



}
