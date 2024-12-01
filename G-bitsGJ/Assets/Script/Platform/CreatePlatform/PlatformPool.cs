using System.Collections.Generic;
using UnityEngine;


public class PlatformPool
{
    //private static PlatformPool instance;
    //public static PlatformPool Instance
    //{
    //    get
    //    {
    //        if (instance == null)
    //        {
    //            instance = new PlatformPool(Resources.Load<PlatformScriptableList>("PlatformList"));
    //        }
    //        return instance;
    //    }
    //}


    private PlatformScriptableList platformScriptableList;
    private Dictionary<PlatformType, List<GameObject>> platformPool = new Dictionary<PlatformType, List<GameObject>>();
    public PlatformPool(PlatformScriptableList platformScriptableList)
    {
        this.platformScriptableList = platformScriptableList;
    }

    public GameObject GetPlatform(PlatformType platformType)
    {
        if (!platformPool.ContainsKey(platformType))
        {
            platformPool.Add(platformType, new List<GameObject>());
        }
        if (platformPool[platformType].Count == 0)
        {
            foreach (var platform in platformScriptableList.platformList)
            {
                if (platform.platformType == platformType)
                {
                    GameObject platformObj = GameObject.Instantiate(platform.platformObj);
                    return platformObj;
                }
            }
        }
        else
        {
            GameObject gameObject = platformPool[platformType][0];
            platformPool[platformType].RemoveAt(0);
            return gameObject;
        }
        return null;
    }

    public void ReducePlatform(GameObject platformObj)
    {
        if (platformObj == null)
        {
            return;
        }
        BasePlatform basePlatform = platformObj.GetComponent<BasePlatform>();
        if (basePlatform)
        {
            if (!platformPool.ContainsKey(basePlatform.platformType))
            {
                platformPool.Add(basePlatform.platformType, new List<GameObject>());
            }
            platformPool[basePlatform.platformType]?.Add(platformObj);
        }
    }
}



