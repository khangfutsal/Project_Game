using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CardUI : MonoBehaviour
{
    [SerializeField] public string name;
    [SerializeField] public Image imgStamina;
    [SerializeField] public TextMeshProUGUI textDescription;
    [SerializeField] public Image imgCollection;
    [SerializeField] public TextMeshProUGUI price;
    [SerializeField] public List<float> dataCard = new List<float>();

    [SerializeField] public Button btnBuy;

    [SerializeField] private PlayerStats playerStats;


    public void SubEvent(CardInfo cardInfo)
    {
        btnBuy.onClick.RemoveAllListeners();
        btnBuy.onClick.AddListener(() => ButtonBuy(cardInfo));
    }

    private void ButtonBuy(CardInfo cardInfo)
    {
        
        ShopUI.GetInstance().CheckStatusCardInfo(cardInfo);

        UpdateDataForPlayer();
        UpdateCollectionGem();



        void UpdateDataForPlayer()
        {
            switch (name)
            {
                case "Damage":
                    {

                        int playerStatsDamage = (int)dataCard[0];
                        playerStats.SetInt_AttackDmg(playerStatsDamage);

                        DataManager.GetInstance().dataPlayerSO.curDamage = playerStatsDamage;
                        break;
                    }
                case "Heart":
                    {
                        playerStats.SetFloat_PercentRegenerationHealth(dataCard[0]);
                        playerStats.timeRegenerationHealth = dataCard[1];

                        DataManager.GetInstance().dataPlayerSO.curDamage = (int)dataCard[0];
                        DataManager.GetInstance().dataPlayerSO.curDamage = (int)dataCard[1];
                        break;
                    }
                case "Mana":
                    {
                        playerStats.statsRegenerationMana = dataCard[0];
                        playerStats.timeRegenerationMana = dataCard[1];

                        DataManager.GetInstance().dataPlayerSO.statsRegenerationMana = dataCard[0];
                        DataManager.GetInstance().dataPlayerSO.timeRegenerationMana = dataCard[1];
                        break;
                    }
                case "SkillDefense":
                    {
                        float curSkillStatus = dataCard[0];
                        playerStats.SetFloat_StatusDefense(curSkillStatus);

                        DataManager.GetInstance().dataPlayerSO.statusDefenseSkill = curSkillStatus;
                        break;
                    }
                case "SkillFireball":
                    {
                        float curSkillStatus = dataCard[0];
                        playerStats.SetFloat_StatusFireBall(curSkillStatus);

                        DataManager.GetInstance().dataPlayerSO.statusFireballSkill = curSkillStatus;
                        break;
                    }
                case "SkillEarthquake":
                    {
                        float curSkillStatus = dataCard[0];
                        playerStats.SetFloat_StatusEarthquake(curSkillStatus);

                        DataManager.GetInstance().dataPlayerSO.statusEarthquakeSkill = curSkillStatus;
                        break;
                    }
                default: break;
            }
        }

        void UpdateCollectionGem()
        {


            float.TryParse(price.text, out float priceValue);
            var typeGem = imgCollection.GetComponent<Image>().sprite.name;
            Debug.Log(typeGem);
            switch (typeGem)
            {
                case "Coin":
                    {
                        var curCoin = GameController.GetInstance().gameManager.GetCoin();

                        if ((float)curCoin >= priceValue)
                        {
                            var remainCoin = curCoin - (int)priceValue;
                            GameController.GetInstance().gameManager.SetCoin(remainCoin);

                            var collection_Ins = Collection_Controller.GetInstance();
                            collection_Ins.StartCoroutine(collection_Ins.TakeCoin(priceValue));
                            
                            transform.gameObject.SetActive(false);

                            cardInfo._isBought = true;
                        }
                        else
                        {
                            Debug.Log("Not enough price");
                            return;
                        }
                        break;
                    }
                case "red_crystal_0001_0":
                    {
                        var curCrystal = GameController.GetInstance().gameManager.GetCrystal();
                        if ((float)curCrystal >= priceValue)
                        {
                            var remainCrystal = curCrystal - (int)priceValue;
                            GameController.GetInstance().gameManager.SetCrystalUI(remainCrystal);

                            Collection_Controller.GetInstance().TakeCrystal(priceValue);
                            transform.gameObject.SetActive(false);
                        }
                        else
                        {
                            Debug.Log("Not enough price");
                            return;
                        }
                        break;
                    }
            }
        }


    }

}
