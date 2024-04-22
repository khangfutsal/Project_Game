using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudUI : MonoBehaviour
{
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderMana;

    private static HudUI _ins;

    public static HudUI GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;
    }

    private void Start()
    {
        sliderHealth.value = 100;
        sliderMana.value = 100;
    }

    public void RegenSliderHealth(float health)
    {
        sliderHealth.value = health;
    }

    public void RegenSliderMana(float mana) {
        sliderMana.value = mana;
    }

    public void TakeSliderHealth(float curHealth)
    {
        sliderHealth.value = curHealth;
    }
    public void TakeSliderMana(float mana)
    {
        sliderMana.value -= mana;
    }
}
