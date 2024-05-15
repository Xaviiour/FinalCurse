using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTools : MonoBehaviour
{
    public GameObject Panel;
    public PolygonCollider2D p;

    public void EnablePanel()
    {
        Panel.SetActive(true);
    }

    public void DisablePanel()
    {
        Panel.SetActive(false);
    }

    public void Inv()
    {
        p.enabled = false;
    }
}
