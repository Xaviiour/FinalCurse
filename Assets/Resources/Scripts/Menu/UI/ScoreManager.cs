using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI sText;
    public TextMeshProUGUI hsText;
    public float sCount;
    public float hsCount;
    public float pPerSec;
    public bool sIncrease;

    private void Start()
    {
        if(PlayerPrefs.GetFloat("HighScore") != null)
        {
            hsCount = PlayerPrefs.GetFloat("HighScore");
        }
    }

    void Update()
    {
        if(sIncrease)
        {
            sCount += pPerSec * Time.deltaTime;
        }

        if(sCount > hsCount)
        {
            hsCount = sCount;
            PlayerPrefs.SetFloat("HighScore", hsCount);
        }

        sText.text = "Score: " + Mathf.Round(sCount);
        hsText.text = "High Score: " + Mathf.Round(hsCount);
    }
}
