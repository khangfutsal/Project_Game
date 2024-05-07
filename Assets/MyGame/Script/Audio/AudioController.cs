using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] public AudioManager manager;

    private static AudioController _ins;

    public static AudioController GetInstance() => _ins;

    private void Awake()
    {
        _ins = this;
        Init();
    }

    private void Update()
    {
        if (PlayerPrefs.HasKey("Volume"))
        {
            var vol = PlayerPrefs.GetFloat("Volume");
            manager.GetAudioSource().volume = vol;
            manager.GetAudioSourceBackground().volume = vol;
        }
    }

    private void Init()
    {
        var aSrcBackground = manager.GetAudioSourceBackground();
        var aClipMenu = manager.GetAudioBMenu();

        StartMusicBackground(aClipMenu, aSrcBackground);
    }

    public void StartMusic(AudioClip aClip, AudioSource aSrc)
    {
        aSrc.Stop();

        aSrc.PlayOneShot(aClip);

    }

    public void StartMusicBackground(AudioClip aClip, AudioSource aSrc)
    {
        aSrc.Stop();
        aSrc.loop = true;
        aSrc.clip = aClip;
        aSrc.Play();

    }

    public void StopMusic()
    {
        var aSrc = manager.GetAudioSource();
        aSrc.Stop();
    }

    public void StopMusicBackground()
    {
        var aSrcBackground = manager.GetAudioSourceBackground();
        aSrcBackground.Stop();
    }
}
