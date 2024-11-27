using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlatformManager
{
    public void CreatePlatform(Vector2 position, PlatformType platformType);
}

public class PlatformManager : IPlatformManager
{
    public float createInterval = 2.0f;

    public float createTimer = 0.0f;

    public List<BasePlatform> platforms = new List<BasePlatform>();
    private bool test = true;
    private int count = 0;
    public void MyUpdate(float deltaTime)
    {
        createTimer += deltaTime;
        if (createTimer >= createInterval)
        {
            createTimer = 0.0f;
            createInterval = Random.Range(1.0f, 2.0f);
            if (test && count >= 1)
            {
                return;
            }
            platforms.Add(CreatePlatformScript.Create());
            count++;
        }

        UpdatePlatform(deltaTime);
    }

    public void UpdatePlatform(float deltaTime)
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            var platform = platforms[i];
            Debug.Log("platform: " + platform.GetHashCode());
            if (platform.gameObject.activeSelf == true)
            {
                platform.MyUpdate(deltaTime);
            }
            else
            {
                CreatePlatformScript.Reduce(platform);
                platforms.RemoveAt(i);
            }
        }
    }

    public void CreatePlatform(Vector2 position, PlatformType platformType)
    {
        platforms.Add(CreatePlatformScript.Create(position, platformType));
    }
}
