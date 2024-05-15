using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioM : MonoBehaviour
{
    public AudioSource BGM;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("SoundManager (1)"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Change(AudioClip music, Scene scene)
    {
        if (scene.buildIndex > 2)
        {
            BGM.Stop();
            BGM.clip = music;
            BGM.Play();
        }
    }
}
