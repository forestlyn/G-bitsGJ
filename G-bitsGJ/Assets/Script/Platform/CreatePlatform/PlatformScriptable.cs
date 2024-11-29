using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlatformScriptable
{
    public PlatformType platformType;
    public float probability;
    public float probabilityDescending;
    public float currentProbability;
    public float minProbability;

    public GameObject platformObj;
}

