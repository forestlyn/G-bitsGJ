using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager:MonoBehaviour
{
    [SerializeField]
    private BGScript bGScript;

    private static BGManager instance;
    public static BGManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void MyUpdate(float deltaTime)
    {
        bGScript?.MyUpdate(deltaTime);
    }

}
