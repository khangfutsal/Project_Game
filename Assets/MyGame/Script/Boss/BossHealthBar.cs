using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private static BossHealthBar _ins;
    [SerializeField] private Slider slider;

    [SerializeField] public float maxValue;

    public static BossHealthBar GetInstance() => _ins;

    private void Start()
    {
        transform.gameObject.SetActive(false);
    }

    private void Awake()
    {
        _ins = this;

        slider = GetComponent<Slider>();
    }

    public void HideHealthBarUI()
    {
        transform.gameObject.SetActive(false);
    }

    public void ShowHealthBarUI()
    {
        transform.gameObject.SetActive(true);
    }


    public void ChangeValueHealth(float _value)
    {
        slider.value = _value;
    }
}
