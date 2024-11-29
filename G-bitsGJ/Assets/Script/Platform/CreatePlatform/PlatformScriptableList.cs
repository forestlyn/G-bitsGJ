using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformScriptableList", menuName = "ScriptableObjects/PlatformScriptableList", order = 1)]
public class PlatformScriptableList : ScriptableObject
{
    public PlatformScriptable[] platformList;
}