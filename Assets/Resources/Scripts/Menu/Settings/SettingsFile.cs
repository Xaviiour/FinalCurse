using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SettingsFile 
{
    public float brightSet;

    public SettingsFile (SO_Settings set)
    {
        brightSet = set.brightValue;
    }
}
