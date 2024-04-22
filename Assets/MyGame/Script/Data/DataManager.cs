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
        FromSOToData();

        string content = JsonUtility.ToJson(dataPlayerPattern);

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
        if (File.Exists(SAVE_PATH + "Save.txt"))
        {
            string content = File.ReadAllText(SAVE_PATH + "Save.txt");
            dataPlayerPattern = JsonUtility.FromJson<DataPlayerPattern>(content);

            FromDataSToSO();
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

    private void FromDataSToSO()
    {

        dataPlayerSO.listCards.Clear();
        dataPlayerSO.curCardsUI.Clear();

        var listCards = dataPlayerPattern.listCardsData;
        var listCardsUI = dataPlayerPattern.curCardsData;

        GetDataCards(listCards, listCardsUI);

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

    void GetDataCards(List<DataCard> listCards, List<DataCard> listCardsUI)
    {
        CardInfo[] asset = Resources.LoadAll<CardInfo>("Cards");
        if (asset != null)
        {
            for (int i = 0; i < asset.Length; i++)
            {
                if (asset[i].id == listCards[i].id)
                {
                    asset[i].name = listCards[i].name;
                    asset[i].description = listCards[i].description;
                    asset[i].price = listCards[i].price;
                    asset[i]._isBought = listCards[i]._isBought;
                    asset[i]._maxLevel = listCards[i]._maxLevel;

                    for (int j = 0; j < listCards[i].dataCard.Count; j++)
                    {
                        asset[i].dataCard[j] = listCards[i].dataCard[j];
                    }

                    dataPlayerSO.listCards.Add(asset[i]);
                }
            }


            for (int i = 0; i < dataPlayerPattern.curCardsData.Count; i++)
            {
                for (int j = 0; j < asset.Length; j++)
                {
                    if (asset[j].id == listCardsUI[i].id)
                    {
                        dataPlayerSO.curCardsUI.Add(asset[j]);
                        break;
                    }
                }
            }
        }
    }

    public void FromSOToData()
    {



        var listCards = dataPlayerSO.listCards;
        var curCardsUI = dataPlayerSO.curCardsUI;

        if (listCards.Count == 0)
        {
            dataPlayerSO.listCards.Clear();
        }
        if (curCardsUI.Count == 0)
        {
            dataPlayerSO.curCardsUI.Clear();
        }

        dataPlayerPattern = new DataPlayerPattern
        {
            curHealth = dataPlayerSO.curHealth,
            percentRegenerationHealth = dataPlayerSO.percentRegenerationHealth,
            timeRegenerationHealth = dataPlayerSO.timeRegenerationHealth,

            curMana = dataPlayerSO.curMana,
            statsRegenerationMana = dataPlayerSO.statsRegenerationMana,
            timeRegenerationMana = dataPlayerSO.timeRegenerationMana,

            curCoin = dataPlayerSO.curCoin,
            curCrystal = dataPlayerSO.curCrystal,

            curDamage = dataPlayerSO.curDamage,
            curScene = dataPlayerSO.curScene,

            statusDefenseSkill = dataPlayerSO.statusDefenseSkill,
            statusEarthquakeSkill = dataPlayerSO.statusEarthquakeSkill,
            statusFireballSkill = dataPlayerSO.statusFireballSkill,

            listCardsData = new List<DataCard>(),
            curCardsData = new List<DataCard>()
        };

        #region Comments
        //dataPlayerPattern.curHealth = dataPlayerSO.curHealth;
        //dataPlayerPattern.percentRegenerationHealth = dataPlayerSO.percentRegenerationHealth;
        //dataPlayerPattern.timeRegenerationHealth = dataPlayerSO.timeRegenerationHealth;

        //dataPlayerPattern.curMana = dataPlayerSO.curMana;
        //dataPlayerPattern.statsRegenerationMana = dataPlayerSO.statsRegenerationMana;
        //dataPlayerPattern.timeRegenerationMana = dataPlayerSO.timeRegenerationMana;

        //dataPlayerPattern.curCoin = dataPlayerSO.curCoin;
        //dataPlayerPattern.curCrystal = dataPlayerSO.curCrystal;

        //dataPlayerPattern.curDamage = dataPlayerSO.curDamage;
        //dataPlayerPattern.curScene = dataPlayerSO.curScene;

        //dataPlayerPattern.statusDefenseSkill = dataPlayerSO.statusDefenseSkill;
        //dataPlayerPattern.statusEarthquakeSkill = dataPlayerSO.statusEarthquakeSkill;
        //dataPlayerPattern.statusFireballSkill = dataPlayerSO.statusFireballSkill;
        #endregion

        for (int i = 0; i < listCards.Count; i++)
        {
            DataCard dataCard = new DataCard
            {
                id = listCards[i].id,
                name = listCards[i].name,
                description = listCards[i].description,
                price = listCards[i].price,
                _isBought = listCards[i]._isBought,
                _maxLevel = listCards[i]._maxLevel,
                dataCard = new List<float>(),
            };



            for (int j = 0; j < listCards[i].dataCard.Count; j++)
            {
                dataCard.dataCard.Add(listCards[i].dataCard[j]);
            }
            dataPlayerPattern.listCardsData.Add(dataCard);
        }

        for (int i = 0; i < curCardsUI.Count; i++)
        {
            DataCard dataCard = new DataCard
            {
                id = curCardsUI[i].id,
                name = curCardsUI[i].name,
                description = curCardsUI[i].description,
                price = curCardsUI[i].price,
                _isBought = curCardsUI[i]._isBought,
                _maxLevel = curCardsUI[i]._maxLevel,
                dataCard = new List<float>(),
            };

            for (int j = 0; j < curCardsUI[i].dataCard.Count; j++)
            {
                dataCard.dataCard.Add(curCardsUI[i].dataCard[j]);
            }
            dataPlayerPattern.curCardsData.Add(dataCard);
        }
    }
}
