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

[CreateAssetMenu(fileName = "PlatformList", menuName = "ScriptableObjects/Platform", order = 1)]
public class PlatformScriptableList : ScriptableObject
{
    public PlatformScriptable[] platformList;
}