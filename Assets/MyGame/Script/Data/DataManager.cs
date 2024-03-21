using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    private static DataManager _ins;

    public DataPlayerSO dataPlayerSO;
    public DataPlayerPattern dataPlayerPattern;

    private string SAVE_PATH;

    public static DataManager GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;
        SAVE_PATH = Application.persistentDataPath + "/Saves/";
        //LoadGame();

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private void Start()
    {
        //LoadGame();
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.B))
        //{
        //    //SceneManager.LoadScene("Chap 2");
        //    SaveGame();
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    LoadData();
        //}
    }

    public void SaveGame()
    {
        string content = JsonUtility.ToJson(dataPlayerSO);

        if (!Directory.Exists(SAVE_PATH))
        {
            Directory.CreateDirectory(SAVE_PATH);
            File.WriteAllText(SAVE_PATH + "Save.txt", content);
        }
        else
        {
            File.WriteAllText(SAVE_PATH + "Save.txt", content);
        }
        
    }

    public void LoadData()
    {
        if(File.Exists(SAVE_PATH + "Save.txt"))
        {
            string content = File.ReadAllText(SAVE_PATH + "Save.txt");
            dataPlayerPattern = JsonUtility.FromJson<DataPlayerPattern>(content);
            //AddDataSO();

        }
        else
        {
            Debug.LogError("Can't LOAD");
        }

    }

    public bool HaveEntranceGameFirst()
    {
        if (File.Exists(SAVE_PATH + "Save.txt"))
        {
            return true;
        }
        return false;
    }

    private void AddDataSO()
    {
        dataPlayerSO.Reset();
        for (int i = 0; i < dataPlayerPattern.listCards.Count; i++)
        {
            dataPlayerSO.listCards.Add(dataPlayerPattern.listCards[i]);
        }

        for (int i = 0; i < dataPlayerPattern.curCardsUI.Count; i++)
        {
            dataPlayerSO.curCardsUI.Add(dataPlayerPattern.curCardsUI[i]);
        }

        dataPlayerSO.curHealth = dataPlayerPattern.curHealth;
        dataPlayerSO.percentRegenerationHealth = dataPlayerPattern.percentRegenerationHealth;
        dataPlayerSO.timeRegenerationHealth = dataPlayerPattern.timeRegenerationHealth;

        dataPlayerSO.curMana = dataPlayerPattern.curMana;
        dataPlayerSO.statsRegenerationMana = dataPlayerPattern.statsRegenerationMana;
        dataPlayerSO.timeRegenerationMana = dataPlayerPattern.timeRegenerationMana;

        dataPlayerSO.curCoin = dataPlayerPattern.curCoin;
        dataPlayerSO.curCrystal = dataPlayerPattern.curCrystal;

        dataPlayerSO.curDamage = dataPlayerPattern.curDamage;
        dataPlayerSO.curScene = dataPlayerPattern.curScene;

        dataPlayerSO.statusDefenseSkill = dataPlayerPattern.statusDefenseSkill;
        dataPlayerSO.statusEarthquakeSkill = dataPlayerPattern.statusEarthquakeSkill;
        dataPlayerSO.statusFireballSkill = dataPlayerPattern.statusFireballSkill;
    }

}
