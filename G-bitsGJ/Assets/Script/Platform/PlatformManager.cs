using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public float createInterval = 2.0f;

    public float createTimer = 0.0f;

    public List<BasePlatform> platforms = new List<BasePlatform>();

    private void Update()
    {
        createTimer += Time.deltaTime;
        if (createTimer >= createInterval)
        {
            createTimer = 0.0f;
            createInterval = Random.Range(1.0f, 2.0f);
            platforms.Add(CreatePlatform.Create());
        }
    }

    public void FixedUpdate()
    {
        for (int i = platforms.Count - 1; i >= 0; i--)
        {
            var platform = platforms[i];
            if (platform.gameObject.activeSelf == true)
            {
                platform.MyFixedUpdate(Time.fixedDeltaTime);
            }
            else
            {
                CreatePlatform.Reduce(platform);
                platforms.RemoveAt(i);
            }
        }
    }

}
