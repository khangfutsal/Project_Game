using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource aSrc;
    [SerializeField] private AudioSource aSrcBackground;

    [SerializeField] private AudioClip aClick;
    [SerializeField] private AudioClip aBMenu;
    [SerializeField] private AudioClip aBGame;
    [SerializeField] private AudioClip aAttack;
    [SerializeField] private AudioClip aBuy;
    [SerializeField] private AudioClip aHit;
    [SerializeField] private AudioClip aEndGame;

    public AudioClip GetAudioClick() => aClick;
    public AudioClip GetAudioBMenu() => aBMenu;
    public AudioClip GetAudioBGame() => aBGame;
    public AudioClip GetAudioAttack() => aAttack;
    public AudioClip GetAudioHit() => aHit;
    public AudioClip GetAudioEndGame() => aEndGame;
    public AudioClip GetAudioBuy() => aBuy;

    public AudioSource GetAudioSource() => aSrc;
    public AudioSource GetAudioSourceBackground() => aSrcBackground;








}
