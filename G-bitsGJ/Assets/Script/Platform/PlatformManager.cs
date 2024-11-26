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

    public void MyUpdate(float deltaTime)
    {
        createTimer += deltaTime;
        if (createTimer >= createInterval)
        {
            createTimer = 0.0f;
            createInterval = Random.Range(1.0f, 2.0f);
            platforms.Add(CreatePlatformScript.Create());
        }
    }

    public void MyFixedUpdate(float fixedDeltaTime)
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            var platform = platforms[i];
            if (platform.gameObject.activeSelf == true)
            {
                platform.MyFixedUpdate(fixedDeltaTime);
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
