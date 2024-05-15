using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonSwitch : MonoBehaviour
{

    public bool unlocked = false;
    public GameObject unlockImage;
    public SceneLoader lvl;

    private void Update()
    {
        UpdateLevelImage();
        UpdateLevelStatus();
    }

    private void UpdateLevelStatus()
    {
        int prevLvlNum = int.Parse(gameObject.name) - 1;
        if (PlayerPrefs.GetInt("Lv" + prevLvlNum.ToString()) > 0)
        {
            unlocked = true;
        }
    }

    private void UpdateLevelImage()
    {
    if (unlocked == false)
        {
            unlockImage.gameObject.SetActive(true);
            lvl.enabled = false;

        }
    else
        {
            unlockImage.gameObject.SetActive(false);
        }
    }
}
