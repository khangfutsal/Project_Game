using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Demon : Boss
{
    [Header("State Machine")]
    public EnemyStateMachine enemyStateMachine = new EnemyStateMachine();

    public Demon_IdleState demon_IdleState;
    public Demon_AttackState demon_AttackState;
    public Demon_EvolutionState demon_EvolutionState;
    public DemonData demonData;
    public DemonStats demonStats;

    [Header("Setup Infor")]
    [SerializeField] private List<BaseSkill> SkillsPhase;
    [SerializeField] private List<GameObject> pointsTele;
    [SerializeField] private BaseSkill curSkill;
    [SerializeField] private float durationSkill;

    [SerializeField] private GameObject curpointsTele;

    [SerializeField] public Phase _curPhase;

    [SerializeField] private float startTimeDelayToAttack;
    [SerializeField] private float endTimeDelayToAttack;
    [SerializeField] public bool _canAttack;
    [SerializeField] public bool _endSkill;

    [Header("Component Variable")]
    private BoxCollider2D boxCollider2D;
    public TransitionDemon transitionDemon;



    private void Awake()
    {
        playerTf = GameObject.Find("BonzePlayer").transform;
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        demonStats = GetComponent<DemonStats>();
        transitionDemon = GetComponent<TransitionDemon>();

        demon_IdleState = new Demon_IdleState(this, enemyStateMachine, demonData, "idle");
        demon_AttackState = new Demon_AttackState(this, enemyStateMachine, demonData, "attack");
        demon_EvolutionState = new Demon_EvolutionState(this, enemyStateMachine, demonData, "Evolution");
    }

    private void Start()
    {
        Initilize();
    }



    private void Update()
    {
        enemyStateMachine.currentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        enemyStateMachine.currentState.PhysicsUpdate();
    }

    public void Initilize()
    {
        curpointsTele = pointsTele[3];
        transform.position = curpointsTele.transform.position;
        Flip();
        InitializePhase(_curPhase);
        enemyStateMachine.Initialize(demon_IdleState);
    }

    #region Phase Function
    public Phase GetPhase() => _curPhase;
    public void InitializePhase(Phase curPhase)
    {
        SkillsPhase.Clear();

        GetSkillInPhase(curPhase);

        #region Local Function

        void GetSkillInPhase(Phase curPhase)
        {
            switch (curPhase)
            {
                case Phase.Phase1:
                    {
                        foreach (var skill in skillsList)
                        {
                            if (skill.phase == 1)
                            {
                                SkillsPhase.Add(skill);
                            }

                        }
                        break;
                    }
                case Phase.Phase2:
                    {
                        foreach (var skill in skillsList)
                        {
                            if (skill.phase == 2)
                            {
                                SkillsPhase.Add(skill);
                            }

                        }
                        break;
                    }
                case Phase.Phase3:
                    {
                        foreach (var skill in skillsList)
                        {
                            if (skill.phase == 3)
                            {
                                SkillsPhase.Add(skill);
                            }

                        }
                        break;
                    }
            }


        }
        #endregion
    }

    public void SetUpPhase()
    {
        float curHealth = demonStats.health;
        switch (_curPhase)
        {
            case Phase.Phase1:
                {
                    if (curHealth < demonStats.maxHealth * 0.7)
                    {
                        InitializePhase(_curPhase);

                        _curPhase = Phase.Phase2;
                        ChangeState();
                    }
                    break;
                }
            case Phase.Phase2:
                {
                    if (curHealth < demonStats.maxHealth * 0.4)
                    {
                        InitializePhase(_curPhase);
                        _curPhase = Phase.Phase3;
                        ChangeState();
                    }
                    break;
                }
        }


        void ChangeState()
        {
            Debug.Log("Change state");
            boxCollider2D.enabled = false;
            enemyStateMachine.ChangeState(demon_EvolutionState);
        }
    }

    public void ModifyPhase(Phase curPhase)
    {
        switch (curPhase)
        {
            case Phase.Phase2:
                {
                    break;
                }
            case Phase.Phase3:
                {
                    break;
                }
        }
    }
    #endregion

    #region Skill Function


    public void DecisionNextSkill()
    {
        float startValueRandom = 0;
        float endValueRandom = 10;
        float random = UnityEngine.Random.Range(startValueRandom, endValueRandom);
        Debug.Log("random : " + random);

        float rating = (float)endValueRandom / SkillsPhase.Count;
        Debug.Log("rating : " + rating);
        for (int i = 0; i < SkillsPhase.Count; i++)
        {
            if (rating == endValueRandom)
            {
                curSkill = SkillsPhase[i];
                PrepareUseSkill(SkillsPhase[i]);
                break;
            }
            else
            {
                float clampValueIndex = Mathf.Clamp(i, 0, SkillsPhase.Count);
                Debug.Log(random + "<" + rating + "*" + (clampValueIndex + 1));
                Debug.Log(random + ">" + rating + "*" + clampValueIndex);
                if ((random < rating * (clampValueIndex + 1)) && (random > rating * clampValueIndex))
                {
                    curSkill = SkillsPhase[i];
                    PrepareUseSkill(SkillsPhase[i]);

                    break;
                }
            }

        }

        void PrepareUseSkill(BaseSkill baseskill)
        {
            float sign;
            if(_curPhase != Phase.Phase3)
            {
                switch (baseskill.typeSkill)
                {
                    case "Range":
                        {
                            for (int i = pointsTele.Count - 2; i < pointsTele.Count; i++)
                            {
                                if (curpointsTele != pointsTele[i])
                                {
                                    sign = curpointsTele.transform.position.x - pointsTele[i].transform.position.x;
                                    Debug.Log(sign);
                                    if (sign != 0)
                                    {
                                        Flip();
                                    }
                                    curpointsTele = pointsTele[i];

                                    transform.position = curpointsTele.transform.position;
                                    MaterialPhase.GetInstance().AppearDissolve();
                                    break;
                                }
                            }
                            break;
                        }
                    case "Melee":
                        {
                            for (int i = 0; i < pointsTele.Count - 2; i++)
                            {
                                if (curpointsTele != pointsTele[i])
                                {
                                    sign = curpointsTele.transform.position.x - pointsTele[i].transform.position.x;
                                    Debug.Log(sign);
                                    if (sign != 0)
                                    {
                                        Flip();
                                    }
                                    curpointsTele = pointsTele[i];
                                    transform.position = curpointsTele.transform.position;
                                    MaterialPhase.GetInstance().AppearDissolve();
                                    break;
                                }
                            }
                            break;
                        }
                    case "MeleeLaunch":
                        {
                            for (int i = 0; i < pointsTele.Count - 2; i++)
                            {
                                if (curpointsTele != pointsTele[i])
                                {
                                    sign = curpointsTele.transform.position.x - pointsTele[i].transform.position.x;
                                    Debug.Log(sign);
                                    if (sign != 0)
                                    {
                                        Flip();
                                    }
                                    curpointsTele = pointsTele[i];
                                    transform.position = curpointsTele.transform.position;
                                    MaterialPhase.GetInstance().AppearDissolve();
                                    break;
                                }
                            }
                            break;
                        }
                }
            }
            else
            {

            }
            
        }


    }


    public void UseSkill()
    {
        if (curSkill == null) return;
        durationSkill = curSkill.duration;
        switch (curSkill.typeSkill)
        {
            case "Melee":
                {
                    anim.SetBool("Mantra", true);
                    break;
                }
            case "Range":
                {
                    anim.SetBool("Launch", true);
                    break;
                }
            case "MeleeLaunch":
                {
                    anim.SetBool("Launch", true);
                    break;
                }
        }
        StartCoroutine(DurationSkill());
        curSkill.UseSkill();

        IEnumerator DurationSkill()
        {
            yield return new WaitForSeconds(durationSkill);
            _endSkill = true;

        }
    }
    #endregion

    public IEnumerator TimeToAttack()
    {
        float random = UnityEngine.Random.Range(startTimeDelayToAttack, endTimeDelayToAttack);
        float curTime = random;
        yield return new WaitForSeconds(curTime);
        _canAttack = true;
    }

    private void OnEnable()
    {
        MaterialPhase.OnAppear.AddListener(UseSkill);
        MaterialPhase.OnDisappear.AddListener(DecisionNextSkill);
    }

    private void OnDisable()
    {
        MaterialPhase.OnAppear.RemoveListener(UseSkill);
        MaterialPhase.OnDisappear.AddListener(DecisionNextSkill);
    }

}


public enum Phase
{
    Phase1,
    Phase2,
    Phase3,
    NONE
}