using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textTitleWave;

    private void OnEnable()
    {
        ResetValue();
    }
    public void ResetValue()
    {
        textTitleWave.color = new Color(textTitleWave.color.r, textTitleWave.color.g, textTitleWave.color.b, 1);
    }


}
