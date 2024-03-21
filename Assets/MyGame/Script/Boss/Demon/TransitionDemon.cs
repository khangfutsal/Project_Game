using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TransitionDemon : MonoBehaviour
{
    private static TransitionDemon _ins;


    public bool isDone;
    [SerializeField] private List<TransitionPhase> transitionPhases;
    [SerializeField] private Transform finalPosSpawnPlayer;

    public static TransitionDemon GetInstance() => _ins;
    private Demon demon;

    private void Awake()
    {
        _ins = this;
        demon = GetComponent<Demon>();
    }

    public IEnumerator ModifyPhase(Phase curPhase)
    {

        yield return new WaitForSeconds(3f);
        StartCoroutine(StartPhase(curPhase));

    }

    public IEnumerator StartPhase(Phase curPhase)
    {
        switch (curPhase)
        {
            case Phase.Phase2:
                {
                    for (int i = 0; i < transitionPhases.Count; i++)
                    {
                        if (curPhase.ToString() == transitionPhases[i].name)
                        {
                            foreach (var vfx in transitionPhases[i].effect)
                            {
                                vfx.SetActive(true);
                            }
                            transitionPhases[i].evolution.SetActive(true);
                            yield return new WaitForSeconds(1f);
                            MaterialPhase.GetInstance().SetShaderPhase(i + 1);
                            break;

                        }
                    }

                    yield return new WaitForSeconds(8f);
                    isDone = true;
                    InActiveEffect();
                    break;
                }
            case Phase.Phase3:
                {
                    Player.GetInstance().playerInputHandler.DisableInput();

                    var skills = demon.skillsList;
                    foreach(var skill in skills)
                    {
                        if(skill.name == "SummonEnemies")
                        {
                            skill.GetComponent<SummonSkill>().UselessSkill();
                        }
                    }

                    for (int i = 0; i < transitionPhases.Count; i++)
                    {
                        if (curPhase.ToString() == transitionPhases[i].name)
                        {
                            foreach (var vfx in transitionPhases[i].effect)
                            {
                                vfx.SetActive(true);
                                if (vfx.transform.childCount > 0)
                                {
                                    foreach (Transform partical in vfx.transform)
                                    {
                                        var obj = partical.GetComponent<ParticleSystem>();
                                        if (obj != null)
                                        {
                                            obj.gameObject.SetActive(true);
                                            obj.Play();
                                        }
                                        else
                                        {
                                            yield return new WaitForSeconds(4f);
                                            partical.gameObject.SetActive(true);
                                        }
                                    }
                                }

                            }
                            yield return new WaitForSeconds(4f);
                            var imageUI = transitionPhases[i].evolution.GetComponent<Image>();
                            imageUI.gameObject.SetActive(true);

                            Sequence sq = DOTween.Sequence();
                            sq.Append(imageUI.DOFade(1, 2f));
                            sq.AppendCallback(() =>
                            {
                                CameraController.GetInstance().ShowCamera("CameraBossFinal");

                                UIController.GetInstance().uiManager.GetDoor().SetActive(false);

                                Player.GetInstance().transform.position = finalPosSpawnPlayer.position;

                                TilemapController.GetInstance().manager.ShowFinalMap();

                                MaterialPhase.GetInstance().SetShaderPhase(i + 1);
                            });
                            sq.AppendInterval(2f);

                            sq.Append(imageUI.DOFade(0, 2f));
                            sq.AppendCallback(() =>
                            {
                                Player.GetInstance().playerInputHandler.ActiveInput();
                                isDone = true;

                                InActiveEffect();


                            });

                            break;
                        }
                    }
                    break;
                }
        }
    }

    public void InActiveEffect()
    {
        Debug.Log("InEffect");
        foreach (var phase in transitionPhases)
        {
            phase.evolution.SetActive(false);
            foreach (var vfx in phase.effect)
            {
                vfx.gameObject.SetActive(false);
            }
        }
    }

}
[Serializable]
public class TransitionPhase
{
    public string name;
    public List<GameObject> effect;
    public GameObject evolution;
}
