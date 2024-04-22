using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuideSkillUI : MonoBehaviour
{
    [SerializeField] public bool _isShowGuide;
    [SerializeField] public List<GameObject> guideSkillsObj;
    [SerializeField] private int countRoutoutine;


    private void Start()
    {
        Init();

    }

    private void Init()
    {
        foreach (var i in guideSkillsObj)
        {
            i.SetActive(false);
        }
    }

    private void Update()
    {
        if (guideSkillsObj[0].activeSelf)
        {
            Debug.Log(guideSkillsObj[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
    }

    public IEnumerator ShowGuideUI(string name)
    {
        countRoutoutine++;
        yield return new WaitUntil(() => !_isShowGuide);
        transform.SetAsLastSibling();
        _isShowGuide = true;

        foreach (var i in guideSkillsObj)
        {
            if (i.name.Contains(name))
            {
                i.SetActive(true);

                while (i.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
                {
                    yield return null;
                }
                _isShowGuide = false;
                countRoutoutine--;
                break;
            }
        }
        if (countRoutoutine == 0)
        {
            transform.gameObject.SetActive(false);
        }


    }



}
