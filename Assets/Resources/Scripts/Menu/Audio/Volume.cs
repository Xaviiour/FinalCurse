using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider vSlider;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("mVol"))
        {
            PlayerPrefs.SetFloat("mVol", 1);
            Load();
        }
        else
            Load();
    }

    public void ChangeVol()
    {
        AudioListener.volume = vSlider.value;
        Save();
    }

    private void Load()
    {
        vSlider.value = PlayerPrefs.GetFloat("mVol");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("mVol", vSlider.value);
    }
}
