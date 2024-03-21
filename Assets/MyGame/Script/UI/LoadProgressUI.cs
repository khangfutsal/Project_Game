using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadProgressUI : MonoBehaviour
{
    private Slider loadSlider;

    private void Awake()
    {
        loadSlider = GetComponent<Slider>();
    }
    private void Update()
    {
        loadSlider.value = LoadSceneManagement.GetLoadingProgress();
    }
}
