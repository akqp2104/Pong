using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip soundtrack;
    public AudioClip[] SFX;

    void Start()
    {
        musicSource.clip = soundtrack;
        musicSource.Play();
    }

    public void RestartBackgroundMusic()
    {
        musicSource.Stop();
        musicSource.Play();
    }

    public void PlaySFX(int sfxIndex)
    {
        SFXSource.PlayOneShot(SFX[sfxIndex]);
    }
}
