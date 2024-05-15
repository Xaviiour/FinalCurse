using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthBar : MonoBehaviour
{
    private Health h;
    public UIStatBar hBar; 

    void Start()
    {
        hBar.SetValue(h.health);
    }

    void Update()
    {
        hBar.SetValue(h.health);
    }
}
