using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    //[SerializeField] private AudioSource soundS;
    //[SerializeField] private AudioSource musicS;
    //[SerializeField] private Sound[] soundS;
    //[SerializeField] private Sound[] musicS;
    private Scene scene;

    private void Awake()
    {
        //instance = this;
        //source = GetComponent<AudioSource>();
        //soundS = GetComponent<AudioSource>();
        //musicS = /*transform.GetChild(0).*/GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        //volumes
        //ChangeMusicV(0);
        //ChangeSoundV(0);
        //InitializeSounds();
    }

    /*private void OnEnable()
    {
        SceneManager.sceneLoaded += PlayTitleMusic;
        SceneManager.sceneLoaded += PlayLevelMusic;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= PlayTitleMusic;
        SceneManager.sceneLoaded -= PlayLevelMusic;
    }

    private void InitializeSounds()
    {
        foreach (Sound s in soundS)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.aClip;
            s.source.pitch = s.pitch;
            s.source.volume = s.vol;
            s.source.loop = s.loop;
        }
    }

    public void PlayClip(string name)
    {
        Sound s = Array.Find(soundS, sound => sound.cName == name);
        if (s == null)
            return;
        if (!s.source.isPlaying)
            s.source.Play();
    }

    public void StopClip(string name)
    {
        Sound s = Array.Find(soundS, sound => sound.cName == name);
        if (s == null)
            return;
        s.source.Stop();
    }

    public void SetVol(string name, float vol)
    {
        Sound s = Array.Find(soundS, sound => sound.cName == name);
        if (s == null)
            return;
        s.source.volume = vol;
    }

    public void SetVol(float vol)
    {
        foreach(Sound s in soundS)
        {
            s.source.volume = vol;
        }
    }

    public float GetVol()
    {
        return soundS[0].source.volume;
    }

    public void SetOverallVol()
    {
        foreach(Sound s in soundS)
        {
            s.source.volume = s.vol;
        }
    }

    public void FadeOut(string name, float fadeTime)
    {
        Sound s = Array.Find(soundS, sound => sound.cName == name);
        StartCoroutine(AudioFadeOut.FadeOut(s.source, fadeTime));
    }

    private void PlayTitleMusic(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 0 && !soundS[3].source.isPlaying)
        {
            PlayClip("main menu");
            StopClip("Level");
            StopClip("Boss");
        }
    }

    private void PlayLevelMusic(Scene scene, LoadSceneMode loadSceneMode)
    {
        if(scene.buildIndex == 6 && !soundS[0].source.isPlaying)
        {
            PlayClip("Level");
            soundS[0].source.volume = 1f;
        }
    }

    /*public void ChangeSoundV(float _change)
    {
        ChangeSourceV(1, "soundVol", _change, soundS);
    }

    public void ChangeMusicV(float _change)
    {
        ChangeSourceV(0.3f, "musicVol", _change, musicS);
    }

    private void ChangeSourceV(float baseV, string volumeN, float change, AudioSource source)
    {
        float currentVol = PlayerPrefs.GetFloat(volumeN, 1);
        currentVol += change;

        if (currentVol > 1)
            currentVol = 0;
        else if (currentVol < 0)
            currentVol = 1;

        float finalVol = currentVol * baseV;
        source.volume = finalVol;

        PlayerPrefs.SetFloat(volumeN, currentVol);
    }*/
}
