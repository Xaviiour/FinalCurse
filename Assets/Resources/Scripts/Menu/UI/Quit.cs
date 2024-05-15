using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    public GameObject cp;
    public GameObject logo;
    public GameObject mb;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void EnableCP()
    {
        cp.SetActive(true);
        logo.SetActive(false);
        mb.SetActive(false);
    }

    public void DisableCP()
    {
        cp.SetActive(false);
        logo.SetActive(true);
        mb.SetActive(true);
    }
}
