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


    public TransitionDemon GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;
    }

    public IEnumerator ModifyPhase(Phase curPhase)
    {
        isDone = false;
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
                                        partical.GetComponent<ParticleSystem>().Play();
                                    }
                                }
                            }
                            yield return new WaitForSeconds(4f);
                            var imageUI = transitionPhases[i].evolution.GetComponent<Image>();
                            imageUI.gameObject.SetActive(true);

                            Sequence sq = DOTween.Sequence();
                            sq.Append(imageUI.DOFade(1, 2f));

                            yield return new WaitForSeconds(2f);

                            sq.Append(imageUI.DOFade(0, 2f));
                            sq.AppendCallback(() =>
                            {
                                isDone = true;

                                imageUI.gameObject.SetActive(false);

                                InActiveEffect();

                                //MaterialPhase.GetInstance().SetShaderPhase(i + 1);
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
