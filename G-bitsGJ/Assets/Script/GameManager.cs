﻿
using System;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager:MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<GameObject>("GameManager").GetComponent<GameManager>();
                instance.Init();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            Init();
        }
    }

    private PlatformManager platformManager;
    private InputManager inputManager;

    [SerializeField]
    private float runSpeed = 1.0f;
    public float RunSpeed
    {
        get { return runSpeed; } 
        set { runSpeed = value; }
    }

    private void Init()
    {
        platformManager = new PlatformManager();
        platformManager.Init();
        inputManager = new InputManager();
        PlayerManager.Instance.CreatePlayer(new Vector2(0, 5));
    }

    private void FixedUpdate()
    {
    }

    public void Update()
    {
        platformManager.MyUpdate(Time.deltaTime * runSpeed);
        BGManager.Instance.MyUpdate(Time.deltaTime * runSpeed);
        inputManager.MyUpdate(Time.deltaTime * runSpeed);
    }

    public void HandleInput(InputType inputType, Vector2 position)
    {
        Debug.Log($"{inputType} {position}");
        switch (inputType)
        {
            case InputType.SwipeLeft:
                platformManager.CreatePlatform(position, PlatformType.ChangeDirectionLeft);
                break;
            case InputType.SwipeRight:
                platformManager.CreatePlatform(position, PlatformType.ChangeDirectionRight);
                break;
            case InputType.SwipeDown:
                platformManager.CreatePlatform(position, PlatformType.Breakable);
                break;
        }
    }
}