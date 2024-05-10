using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip buttonclick_SFX;
    public AudioClip explosion_SFX;
    public AudioClip gameover_SFX;
    public AudioSource BGM;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ButtonClick()
    {
        audioSource.clip = buttonclick_SFX;
        audioSource.Play();
    }

    public void Explosion(AudioSource audio)
    {
        audio.clip = explosion_SFX;
        audio.Play();
    }

    public void GameOver()
    {
        BGM.Stop();
        audioSource.clip = gameover_SFX;
        audioSource.Play();
    }
}
