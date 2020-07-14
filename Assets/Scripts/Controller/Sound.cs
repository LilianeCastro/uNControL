using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] gameSound;
    public AudioClip[] fx;
    public AudioClip[] fxDeath;

    public void changeSong(string sceneName)
    {
        audioSource.Stop();
        if(sceneName.Equals("mainMenu") || sceneName.Equals("gameOver"))
        {
            audioSource.clip = gameSound[0];
        }
        else if(sceneName.Equals("inGame"))
        {
            audioSource.clip = gameSound[1];
        }
        if(audioSource.clip!=null)
        {
            audioSource.Play();
        }
    }

    public void playFx(int idFx)
    {
        audioSource.PlayOneShot(fx[idFx]);
    }

    public void playDeathRandom()
    {
        audioSource.PlayOneShot(getRandomFx(fxDeath));
    }

    public float getAudioSourceVol()
    {
        return audioSource.volume;
    }

    public void setAudioSourceVol(float newVol)
    {
        audioSource.volume = newVol;
    }

    private AudioClip getRandomFx(AudioClip[] audioClip)
    {
        int random = Random.Range(0, audioClip.Length);
        return audioClip[random];
    }

}
