using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour, IDmgable
{

    [field: SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float health { get; set; }

    [SerializeField] private int damageAttack;

    [Header("Regeneration Properties")]
    [SerializeField] public float percentRegenerationHealth;
    [SerializeField] public float timeRegenerationHealth;
    [SerializeField] private float startTimeRegenerationHealth;
    [SerializeField] private bool _isHealthy;

    [Header("Mana Properties")]
    [SerializeField] public float statsRegenerationMana;
    [SerializeField] public float timeRegenerationMana;
    [SerializeField] private float startTimeRegenerationMana;
    [SerializeField] private bool _isRegenerateMana;

    [SerializeField] private float maxMana;
    [SerializeField] public float mana;

    [Header("Skill Properties")]
    [SerializeField] private float statusFireballSkill;
    [SerializeField] private float statusEarthquakeSkill;
    [SerializeField] private float statusDefenseSkill;

    [Header("Ice Skill Properties")]
    [SerializeField] public float speed;
    [SerializeField] private float durationIce;
    [SerializeField] public bool _durationIce;

    [Header("Fire Skill Properties")]
    [SerializeField] private float durationFire;
    [SerializeField] private float timesHitFireEffect;
    [SerializeField] private float damageFire;


    private Player player;


    #region Get Function
    public float GetFloat_PercentRegenerationHealth() => percentRegenerationHealth;
    public float GetFloat_StatusDefense() => statusDefenseSkill;
    public float GetFloat_StatusFireBall() => statusFireballSkill;
    public float GetFloat_StatusEarthquake() => statusEarthquakeSkill;
    public int GetInt_AttackDmg() => damageAttack;
    #endregion

    #region Set Function
    public void SetFloat_StatusDefense(float _status) => statusDefenseSkill = _status;
    public void SetFloat_StatusFireBall(float _status) => statusFireballSkill = _status;
    public void SetFloat_StatusEarthquake(float _status) => statusEarthquakeSkill = _status;
    public void SetFloat_PercentRegenerationHealth(float _health) => percentRegenerationHealth = _health;
    public void SetInt_AttackDmg(int dmg) => damageAttack = dmg;
    #endregion



    private void Awake()
    {
        player = GetComponent<Player>();
    }



    private void Update()
    {

        RegenerationHealth();
        RegenerationMana();
    }



    private void Start()
    {
        Init();
    }

    private void Init()
    {

        damageAttack = DataManager.GetInstance().dataPlayerSO.curDamage;
        percentRegenerationHealth = DataManager.GetInstance().dataPlayerSO.percentRegenerationHealth;
        timeRegenerationHealth = DataManager.GetInstance().dataPlayerSO.timeRegenerationHealth;
        statsRegenerationMana = DataManager.GetInstance().dataPlayerSO.statsRegenerationMana;
        timeRegenerationMana = DataManager.GetInstance().dataPlayerSO.timeRegenerationMana;
        statusDefenseSkill = DataManager.GetInstance().dataPlayerSO.statusDefenseSkill;
        statusEarthquakeSkill = DataManager.GetInstance().dataPlayerSO.statusEarthquakeSkill;
        statusFireballSkill = DataManager.GetInstance().dataPlayerSO.statusFireballSkill;

        health = DataManager.GetInstance().dataPlayerSO.curHealth;
        mana = DataManager.GetInstance().dataPlayerSO.curMana;
    }

    public void HitEffectIceSkill()
    {
        Debug.Log("Ice skill");
        _durationIce = true;
        StartCoroutine(IceEffect());

        IEnumerator IceEffect()
        {
            yield return new WaitForSeconds(durationIce);
            _durationIce = false;

        }
    }

    public void HitEffectFireSkill()
    {
        Debug.Log("Fire skill");
        StartCoroutine(FireEffect());

        IEnumerator FireEffect()
        {
            for (int i = 0; i < timesHitFireEffect; i++)
            {
                yield return new WaitForSeconds(durationFire);
                TakeDamage(damageFire);
            }
        }
    }



    public void RegenerationMana()
    {
        if (mana < 100 && mana >= 0)
        {
            _isRegenerateMana = true;
            if (CanRegenerationMana())
            {
                float currentMana = mana;
                float minMana = 0;

                float increaseMana = Mathf.Clamp((currentMana + statsRegenerationMana), minMana, maxMana);
                Debug.Log("Mana : " + statsRegenerationMana + " " + currentMana);
                Debug.Log("Mana : " + increaseMana);
                HudUI.GetInstance().RegenSliderMana(increaseMana);
                mana = increaseMana;

                DataManager.GetInstance().dataPlayerSO.curMana = mana;
            }

        }
        else
        {
            _isRegenerateMana = false;
        }
        bool CanRegenerationMana()
        {
            if (_isRegenerateMana)
            {
                startTimeRegenerationMana -= Time.deltaTime;
                while (startTimeRegenerationMana < 0)
                {
                    startTimeRegenerationMana = timeRegenerationMana;
                    return true;
                }
            }
            return false;

        }
    }

    public void TakeMana(float _mana)
    {
        mana -= _mana;
        DataManager.GetInstance().dataPlayerSO.curMana = mana;
    }
    public void RegenerationHealth()
    {
        if (health < 100 && health > 0)
        {
            _isHealthy = true;
            if (CanRegenerationHealth())
            {
                float percentHealth = GetFloat_PercentRegenerationHealth();
                float currentHealth = health;
                float minHealth = 0;

                float increaseHealth = Mathf.Clamp(currentHealth + (percentHealth / 100), minHealth, maxHealth);
                HudUI.GetInstance().RegenSliderHealth(increaseHealth);
                health = increaseHealth;
                DataManager.GetInstance().dataPlayerSO.curHealth = health;
            }

        }
        else
        {
            _isHealthy = false;
        }
        bool CanRegenerationHealth()
        {
            if (_isHealthy)
            {
                startTimeRegenerationHealth -= Time.deltaTime;
                while (startTimeRegenerationHealth < 0)
                {
                    startTimeRegenerationHealth = timeRegenerationHealth;
                    return true;
                }
            }
            return false;

        }

    }



    public void Die()
    {
        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aEnd = AudioController.GetInstance().manager.GetAudioEndGame();
        AudioController.GetInstance().StopMusicBackground();
        AudioController.GetInstance().StartMusic(aEnd, aSrc);

        player.SetBool_IsDeath(true);

        player.SetVelocityX(0);

        StartCoroutine(DelayEnd());
        IEnumerator DelayEnd()
        {
            var pauseUI = UIController.GetInstance().uiManager.GetPauseUI();
            var shopUI = UIController.GetInstance().uiManager.GetShopUI();
            var endUI = UIController.GetInstance().uiManager.GetEndUI();
            pauseUI.SetActive(false);
            shopUI.SetActive(false);
            endUI.SetActive(true);

            endUI.transform.SetAsLastSibling();
            yield return new WaitForSeconds(2f);

            Time.timeScale = 0;
        }
    }

    public void TakeDamage(float dmg, Transform tf = null)
    {

        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aClipHit = AudioController.GetInstance().manager.GetAudioHit();

        AudioController.GetInstance().StartMusic(aClipHit, aSrc);

        CameraShake.GetInstance().ShakeCamera(3, .5f, .1f);
        if (tf == null)
        {
            if(dmg >= health)
            {
                health = 0;
            }
            else
            {
                health -= dmg;
            }
            HudUI.GetInstance().TakeSliderHealth(health);



            DataManager.GetInstance().dataPlayerSO.curHealth = health;

            player.SetBool_IsHurt(true);
            if (health == 0) { Die(); health = 0; }
            return;
        }

        Vector3 directionToTarget = (tf.position - transform.position).normalized;

        float dotProduct = Vector3.Dot(transform.right, directionToTarget);
        bool defenseInput = player.playerInputHandler.defenseInput;

        float totalDamage;
        //Debug.Log("Player Dot : " + dotProduct);
        if (defenseInput)
        {
            if (dotProduct > .3f)
            {
                totalDamage = dmg * .8f;
                player.SetBool_IsHurt(true);
                if (totalDamage >= health)
                {
                    health = 0;
                }
                else
                {
                    health -= totalDamage;
                }
                HudUI.GetInstance().TakeSliderHealth(health);
                DataManager.GetInstance().dataPlayerSO.curHealth = health;

                if (health <= 0) { Die(); health = 0; }
                return;
            }
            else
            {
                totalDamage = dmg;
                player.SetBool_IsHurt(true);
                if (totalDamage >= health)
                {
                    health = 0;
                }
                else
                {
                    health -= totalDamage;
                }
                DataManager.GetInstance().dataPlayerSO.curHealth = health;
                HudUI.GetInstance().TakeSliderHealth(health);
                if (health <= 0) { Die(); health = 0; }

                player.playerStateMachine.ChangeState(player.playerTakeDamageState);
                return;
            }
        }
        totalDamage = dmg;
        player.SetBool_IsHurt(true);

        if (totalDamage >= health)
        {
            health = 0;
        }
        else
        {
            health -= totalDamage;
        }
        HudUI.GetInstance().TakeSliderHealth(health);
        DataManager.GetInstance().dataPlayerSO.curHealth = health;
        if (health <= 0) { Die(); health = 0; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("interact"))
        {
            Iinteraction Iinteract = collision.GetComponent<Iinteraction>();
            if (Iinteract != null)
            {
                Iinteract.interact();
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("interact"))
        {
            Iinteraction Iinteract = collision.GetComponent<Iinteraction>();
            Debug.Log(Iinteract);
            if (Iinteract != null)
            {
                Iinteract.interact();
            }
        }
    }


}
