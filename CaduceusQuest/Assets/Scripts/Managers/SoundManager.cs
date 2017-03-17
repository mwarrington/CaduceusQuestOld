using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource BGMChannel,
                       AmbientChannel,
                       HUDSFX,
                       MiscSFX;

    public void PlayBGM(AudioClip clip)
    {
        BGMChannel.clip = clip;
        BGMChannel.Play();
    }

    public void PlayAmbient(AudioClip clip)
    {
        AmbientChannel.clip = clip;
        AmbientChannel.Play();
    }

    public void PlayHUDSFX(AudioClip clip)
    {
        HUDSFX.clip = clip;
        HUDSFX.Play();
    }

    public void PlayMiscSFX(AudioClip clip)
    {
        MiscSFX.clip = clip;
        MiscSFX.Play();
    }

    public void PauseBGM()
    {
        BGMChannel.Pause();
    }

    public void PauseAmbient()
    {
        AmbientChannel.Pause();
    }

    public void PauseHUDSFX()
    {
        HUDSFX.Pause();
    }

    public void PauseMiscSFX()
    {
        MiscSFX.Pause();
    }
}