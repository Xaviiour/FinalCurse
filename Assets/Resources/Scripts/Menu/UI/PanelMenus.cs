using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelMenus : MonoBehaviour
{
    //menu
    public GameObject story;
    public GameObject logo;
    public GameObject mb; //menu buttons
    public GameObject ui;

    //pause
    public GameObject pb; //pause button
    public GameObject pm; //pause menu


    //setting
    public GameObject setting;

    //Story menu
    public void EnableSP()
    {
        story.SetActive(true);
        logo.SetActive(false);
        mb.SetActive(false);
    }

    public void DisableSP()
    {
        story.SetActive(false);
        logo.SetActive(true);
        mb.SetActive(true);
    }

    //Pause menu
    public void EnablePM(bool status)
    {
        pm.SetActive(status);
        pb.SetActive(false);
        ui.SetActive(false);

        if(status)
        {
            Time.timeScale = 0;
        }
    }

    public void DisablePM(bool status)
    {
        pm.SetActive(!status);
        pb.SetActive(true);
        ui.SetActive(true);

        if (status)
        {
            Time.timeScale = 1;
        }
    }

    public void ReturnPage(bool status)
    {
        if(status)
        {
            Time.timeScale = 1;
        }
    }

    public void PauseQuit(bool status)
    {
        if (status)
        {
            pm.SetActive(false);
        }
    }

    //Setting Pause
    public void EnableSet(bool status)
    {
        if(status)
        {
            pm.SetActive(false);
            setting.SetActive(true);
        }
    }
    public void DisableSet(bool status)
    {
        if (status)
        {
            pm.SetActive(true);
            setting.SetActive(false);
        }
    }
}
