using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointEnterUI : MonoBehaviour, IPointerEnterHandler
{

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundClick();
    }


    private static void SoundClick()
    {
        var aSrc = AudioController.GetInstance().manager.GetAudioSource();
        var aClipClick = AudioController.GetInstance().manager.GetAudioClick();

        AudioController.GetInstance().StartMusic(aClipClick, aSrc);
    }

}
