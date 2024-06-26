using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorInteract_Chap1 : MonoBehaviour, Iinteraction
{
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private string sceneName;

    public void interact()
    {
        if (doorOpen.activeSelf)
        {
            var aSrcBackground = AudioController.GetInstance().manager.GetAudioSourceBackground();
            var aClipGame = AudioController.GetInstance().manager.GetAudioBGame();
            LoadSceneManagement.LoadScene(sceneName, aClipGame, aSrcBackground);
        }
    }

    public void ActiveDoorOpen()
    {
        doorOpen.SetActive(true);
    }




}
