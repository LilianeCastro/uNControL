using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoSingleton<Sound>
{
    public AudioSource audioSource;
    public AudioClip[] gameSound;
    public AudioClip[] fx;
    public AudioClip[] fxDeath;

    public override void Init()
    {
        base.Init();
        audioSource.volume = 0.5f;
    }

    void Start() {
        changeSong("mainMenu");
    }

    public void changeSong(string sceneName)
    {
        audioSource.Stop();
        if(sceneName.Equals("mainMenu"))
        {
            audioSource.clip = gameSound[0];
        }
        else if(sceneName.Equals("inGame"))
        {
            playFx(1);
            audioSource.clip = gameSound[1];
        }
        else if(sceneName.Equals("gameOver"))
        {
            playFx(2);
            audioSource.clip = gameSound[2];
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
