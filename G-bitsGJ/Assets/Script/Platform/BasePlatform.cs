using System;
using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public PlatformType platformType;


    public virtual void Reduce()
    {
        gameObject.SetActive(false);
    }


    public virtual void ReInit(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public virtual void MyFixedUpdate(float fixedDeltaTime)
    {
        // 计算物体在世界空间中的移动量
        Vector3 movement = speed * Vector3.up * Time.fixedDeltaTime;
        //Debug.Log("movement: " + movement+" "+transform.position);
        // 将移动量应用到物体的位置上
        transform.Translate(movement);

        if (transform.position.y > 6)
        {
            Reduce();
        }
    }
}
