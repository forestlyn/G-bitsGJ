using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IPlatformManager
{
    public void CreatePlatform(Vector2 position, PlatformType platformType);
}

public class PlatformManager : IPlatformManager
{
    public float createLeftInterval = 1.0f;
    public float createRightInterval = 1.5f;
    public float createInterval = 1.0f;
    public float createTimer = 0.0f;

    public List<BasePlatform> platforms = new List<BasePlatform>();
    //private bool test = false;
    //private int count = 0;

    public PlatformManager(float createLeftInterval,float createRightInterval)
    {
        this.createInterval = Random.Range(createLeftInterval, createRightInterval);
        this.createLeftInterval = createLeftInterval;
        this.createRightInterval = createRightInterval;
    }

    public void Init()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Platform");
        foreach (var gameObject in gameObjects)
        {
            platforms.Add(gameObject.GetComponent<BasePlatform>());
        }
    }

    public void MyUpdate(float deltaTime)
    {
        createTimer += deltaTime;
        if (createTimer >= createInterval)
        {
            createTimer = 0.0f;
            createInterval = Random.Range(createLeftInterval, createRightInterval);
            //if (test && count >= 1)
            //{
            //    return;
            //}
            platforms.Add(CreatePlatformScript.Create());
            //count++;
        }

        UpdatePlatform(deltaTime);
    }

    
    public void UpdatePlatform(float deltaTime)
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            var platform = platforms[i];
            //Debug.Log("platform: " + platform.GetHashCode());
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
