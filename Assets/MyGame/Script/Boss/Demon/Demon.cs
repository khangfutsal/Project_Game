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
    public Demon_DeathState demon_DeathState;
    public DemonData demonData;
    public DemonStats demonStats;

    [Header("Setup Infor")]
    [SerializeField] private List<BaseSkill> SkillsPhase;
    [SerializeField] private List<PositionsBoss> points;
    [SerializeField] private List<GameObject> curPoints;
    [SerializeField] private BaseSkill curSkill;
    [SerializeField] private float durationSkill;

    [SerializeField] private GameObject curpointsTele;

    [SerializeField] public Phase _curPhase;

    [SerializeField] private float startTimeDelayToAttack;
    [SerializeField] private float endTimeDelayToAttack;
    [SerializeField] public bool _canAttack;
    [SerializeField] public bool _endSkill;

    [SerializeField] private bool _isDeath;

    [Header("Component Variable")]
    public BoxCollider2D boxCollider2D;
    public TransitionDemon transitionDemon;
    public SpriteRenderer spriteRenderer;


    public void SetBool_IsDeath(bool _bool) => _isDeath = _bool;

    public bool GetBool_IsDeath() => _isDeath;

    private void Awake()
    {
        playerTf = GameObject.Find("BonzePlayer").transform;
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        demonStats = GetComponent<DemonStats>();
        transitionDemon = GetComponent<TransitionDemon>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        demon_IdleState = new Demon_IdleState(this, enemyStateMachine, demonData, "idle");
        demon_AttackState = new Demon_AttackState(this, enemyStateMachine, demonData, "attack");
        demon_EvolutionState = new Demon_EvolutionState(this, enemyStateMachine, demonData, "Evolution");
        demon_DeathState = new Demon_DeathState(this, enemyStateMachine, demonData, "Death");
    }

    private void Start()
    {
        Initilize();
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            enemyStateMachine.Initialize(demon_IdleState);
        }
        if (enemyStateMachine.currentState == null) return;
        enemyStateMachine.currentState.LogicUpdate();
    }

    private void SetFacingDirection()
    {
        if (transform.rotation.y == 0)
        {
            facingDirection = -1;
        }
        else
        {
            facingDirection = 1;
        }
    }

    private void FixedUpdate()
    {
        if (enemyStateMachine.currentState == null) return;
        enemyStateMachine.currentState.PhysicsUpdate();
    }

    public void Initilize()
    {
        SetUpPositions();
        curpointsTele = points[0].positions[3];
        transform.position = curpointsTele.transform.position;
        transform.rotation = curpointsTele.transform.rotation;

        InitializePhase(_curPhase);

        

    }

    public void ActiveControls()
    {
        enemyStateMachine.Initialize(demon_IdleState);
    }

    public void SetUpPositions()
    {
        curPoints.Clear();
        if (_curPhase != Phase.Phase3)
        {
            for (int i = 0; i < points[0].positions.Count; i++)
            {
                curPoints.Add(points[0].positions[i]);
            }
        }
        else
        {
            for (int i = 0; i < points[1].positions.Count; i++)
            {
                curPoints.Add(points[1].positions[i]);
            }
            curpointsTele = curPoints[1];
            Flip();
        }
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
                    Debug.Log("12");
                    PrepareUseSkill(SkillsPhase[i]);

                    break;
                }
            }

        }

        void PrepareUseSkill(BaseSkill baseskill)
        {
            Debug.Log("34");
            if (_curPhase != Phase.Phase3)
            {
                switch (baseskill.sidePosition)
                {
                    case "Top":
                        {
                            for (int i = curPoints.Count - 2; i < curPoints.Count; i++)
                            {
                                if (curpointsTele != curPoints[i])
                                {

                                    curpointsTele = curPoints[i];

                                    transform.position = curpointsTele.transform.position;
                                    transform.rotation = curpointsTele.transform.rotation;
                                    MaterialPhase.GetInstance().AppearDissolve();
                                    break;
                                }
                            }
                            break;
                        }
                    case "Bot":
                        {
                            for (int i = 0; i < curPoints.Count - 2; i++)
                            {
                                if (curpointsTele != curPoints[i])
                                {

                                    curpointsTele = curPoints[i];
                                    transform.position = curpointsTele.transform.position;
                                    transform.rotation = curpointsTele.transform.rotation;
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
                Debug.Log(baseskill.sidePosition);
                switch (baseskill.sidePosition)
                {
                    case "Top":
                        {
                            for (int i = curPoints.Count - 1; i < curPoints.Count; i++)
                            {
                                Debug.Log(curPoints[i].name);
                                curpointsTele = curPoints[i];

                                transform.position = curpointsTele.transform.position;
                                transform.rotation = curpointsTele.transform.rotation;
                                MaterialPhase.GetInstance().AppearDissolve();
                                break;
                            }
                            break;
                        }
                    case "Bot":
                        {
                            for (int i = 0; i < curPoints.Count - 1; i++)
                            {
                                if (curpointsTele != curPoints[i])
                                {
                                    curpointsTele = curPoints[i];
                                    transform.position = curpointsTele.transform.position;
                                    transform.rotation = curpointsTele.transform.rotation;
                                    MaterialPhase.GetInstance().AppearDissolve();
                                    break;
                                }
                            }
                            break;
                        }
                }
            }

            SetFacingDirection();
        }


    }


    public void UseSkill()
    {
        if (curSkill == null) return;
        durationSkill = curSkill.duration;
        switch (curSkill.typeSkill)
        {
            case "Mantra":
                {
                    anim.SetBool("Mantra", true);
                    break;
                }
            case "Launch":
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
        Debug.Log("Useskill test");

        IEnumerator DurationSkill()
        {
            yield return new WaitForSeconds(durationSkill);
            _endSkill = true;

        }
    }
    #endregion

    public void TransitionDeath()
    {
        Sequence sq = DOTween.Sequence();
        sq.Append(spriteRenderer.DOFade(0, 3f));
        sq.AppendCallback(() => {
            Collection_Controller.GetInstance().SpawnGem(transform);
            transform.gameObject.SetActive(false);
            BossHealthBar.GetInstance().HideHealthBarUI();
        });
    }

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
        MaterialPhase.OnDisappear.RemoveListener(DecisionNextSkill);
    }

}
[Serializable]
public class PositionsBoss
{
    public List<GameObject> positions;
}
public enum Phase
{
    Phase1,
    Phase2,
    Phase3,
    NONE
}