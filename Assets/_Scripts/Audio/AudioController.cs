using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public static AudioController instance;

    public AudioClip audioDoVat;
    public AudioClip audioHuongDan;
    public AudioClip audioStepPlayer;
    public AudioClip audioStorm;
    public AudioClip audioWind;
    
    public AudioSource Music;
    public AudioSource SoundPlayer;
    public AudioSource SoundDoVat;
    public AudioSource SoundEnvironment;
    private void Awake()
    {
        instance = this;
    }
    public void OnAudioDoVat()
    {
        SoundDoVat.PlayOneShot(audioDoVat);
    }
    public void OnAudioHuongDan()
    {
        SoundDoVat.PlayOneShot(audioHuongDan);
    }
    public void OnAudioStepPlayer()
    {
        SoundPlayer.PlayOneShot(audioStepPlayer);
    }
    public void OnAudioObject(AudioClip clip) 
    {
        SoundDoVat.PlayOneShot(clip);
    }

    public void OnAudioStorm()
    {
        SoundEnvironment.PlayOneShot(audioStorm);
    }

    public void OnAudioWind()
    {
        SoundEnvironment.PlayOneShot(audioWind);
    }
    public string ChangeVolumeAudio()
    {
        if (SoundDoVat.mute)
        {
            Music.mute = false;
            SoundDoVat.mute = false;
            SoundPlayer.mute = false;
            SoundEnvironment.mute = false;
            return "Tắt âm";
        }
        else if(!SoundDoVat.mute)
        {
            Music.mute = true;
            SoundDoVat.mute = true;
            SoundPlayer.mute = true;
            SoundEnvironment.mute = true;
            return "Bật âm";
        }

        return "";
    }
}
