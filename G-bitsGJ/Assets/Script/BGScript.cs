using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGScript : MonoBehaviour
{
    public float DisapperYPos;
    public Vector2 BeginPos;

    public float Speed = 2f;
    public void MyUpdate(float deltaTime)
    {
        if(transform.position.y >= DisapperYPos)
        {
            transform.position = new Vector2(BeginPos.x, BeginPos.y);
        }
        transform.position += new Vector3(0, Speed * deltaTime, 0);
    }


}
