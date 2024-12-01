
using System;
using System.Net.NetworkInformation;
using Unity.Mathematics;
using UnityEngine;


public class CreatePlatformScript : MonoBehaviour
{
    private PlatformScriptableList platformList;
    [SerializeField]
    private Transform[] createPoints;

    private static CreatePlatformScript instance;

    private float totalProbability;

    private PlatformPool platformPool;


    private static void CreateIfNotExist()
    {
        if (instance == null)
        {
            GameObject go = Resources.Load<GameObject>("CreatePlatform");
            if (go == null)
            {
                Debug.LogError("CreatePlatform prefab not found");
                return;
            }
            instance = Instantiate(go).GetComponent<CreatePlatformScript>();
            instance.platformList = Resources.Load<PlatformScriptableList>("SO/PlatformList");
            foreach (var platform in instance.platformList.platformList)
            {
                platform.currentProbability = platform.probability;
                instance.totalProbability += platform.currentProbability;
            }
            instance.platformPool = new PlatformPool(instance.platformList);
        }
    }

    public static void Reset()
    {
        if (instance != null)
        {
            foreach (var platform in instance.platformList.platformList)
            {
                platform.currentProbability = platform.probability;
                instance.totalProbability += platform.currentProbability;
            }
        }
        else
        {
            CreateIfNotExist();
        }
    }

    public static BasePlatform Create()
    {
        CreateIfNotExist();

        Vector3 position = GetRandomPosition();
        GameObject platform = GetRandomPlatform();
        if (platform != null)
        {
            platform.GetComponent<BasePlatform>().ReInit(position);
        }
        return platform.GetComponent<BasePlatform>();
    }

    public static BasePlatform Create(Vector2 position, PlatformType platformType)
    {
        CreateIfNotExist();

        GameObject platform = instance.platformPool.GetPlatform(platformType);
        if (platform != null)
        {
            platform.GetComponent<BasePlatform>().ReInit(position);
        }
        return platform.GetComponent<BasePlatform>();
    }

    private static GameObject GetRandomPlatform()
    {
        CreateIfNotExist();
        float randomValue = UnityEngine.Random.Range(0, instance.totalProbability);
        float tmpProbability = 0;
        foreach (var platform in instance.platformList.platformList)
        {
            tmpProbability += platform.currentProbability;
            if (randomValue <= tmpProbability)
            {
                if (platform.currentProbability - platform.probabilityDescending >= platform.minProbability)
                {
                    platform.currentProbability -= platform.probabilityDescending;
                    instance.totalProbability -= platform.probabilityDescending;
                }
                //Debug.Log("Create platform: " + platform.platformType);
                return instance.platformPool.GetPlatform(platform.platformType);
            }
        }
        return null;
    }

    public static Vector3 GetRandomPosition()
    {
        CreateIfNotExist();
        int randomIndex = UnityEngine.Random.Range(0, instance.createPoints.Length);
        Transform createPoint = instance.createPoints[randomIndex];
        return createPoint.position;
    }

    public static void Reduce(BasePlatform platform)
    {
        CreateIfNotExist();
        //Debug.Log($"instance is {instance != null} platformPool is {instance.platformPool != null}");
        if (instance != null && platform != null)
            instance.platformPool.ReducePlatform(platform.gameObject);
    }
}
