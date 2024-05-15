using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering.PostProcessing;

public class Settings : MonoBehaviour
{
    public PostProcessProfile profile;
    public SO_Settings settings;
    public SO_Settings defaultSet;
    public SettingsFile setFile;

    public TMPro.TMP_Dropdown resoDrop;
    Resolution[] resos;

    public SliderSettings brightness;

    #region Starting
    //reso start
    private void Start()
    {
        LoadSettings();
        ButtonEvents();
        resos = Screen.resolutions;
        resoDrop.ClearOptions();

        List<string> options = new List<string>();

        int curResoIndex = 0;
        for (int i = 0; i < resos.Length; i++)
        {
            string option = resos[i].width + "x" + resos[i].height;
            options.Add(option);

            if (resos[i].width == Screen.currentResolution.width && resos[i].height == Screen.currentResolution.height)
            {
                curResoIndex = i;
            }
        }
        resoDrop.AddOptions(options);
        resoDrop.value = curResoIndex;
        resoDrop.RefreshShownValue();
    }

    void LoadSettings()
    {
        LoadSettingsFile.settings = settings;
        LoadSettingsFile.defaultSet = defaultSet;
        LoadSettingsFile.InitiateSettings();

        brightness.slider.value = settings.brightValue;
        Brightness(settings.brightValue);
    }

    void ButtonEvents()
    {
        brightness.slider.onValueChanged.AddListener(delegate { Brightness(brightness.slider.value); });
    }
    #endregion

    void Brightness(float curValue)
    {
        float finalValue = SliderConversion(brightness, curValue);
        profile.GetSetting<ColorGrading>().postExposure.Override(finalValue);
        settings.brightValue = curValue;
    }


    public void SaveBright()
    {
        PlayerPrefs.SetFloat("mBright", brightness.slider.value);
    }

    public void LoadBright()
    {
        PlayerPrefs.GetFloat("mBright", brightness.slider.value);
    }

    public void SetReso(int resoIndex)
    {
        Resolution reso = resos[resoIndex];
        Screen.SetResolution(reso.width, reso.height, Screen.fullScreen);
    }

    public void IsFullScreen(bool isFS)
    {
        Screen.fullScreen = isFS;
    }

    float SliderConversion(SliderSettings set, float curVal)
    {
        float conversion = ConvertValue(set.slider.minValue, set.slider.maxValue, set.minSetVal, set.maxSetVal, curVal);
        return conversion;
    }

    float ConvertValue(float virtMin, float virtMax, float actMin, float actMax, float curValue)
    {
        float ratio = (actMax - actMin) / (virtMax - virtMin);
        float returnVal = ((curValue * ratio) - (virtMin * ratio)) + actMin;
        return returnVal;
    }
}
