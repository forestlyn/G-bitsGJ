using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinePlatform : BasePlatform
{
    public bool isPlayerOn = false;

    public float Attack = 2f;
    public override void MyUpdate(float deltaTime)
    {
        base.MyUpdate(deltaTime);
        if (isPlayerOn)
        {
            // 攻击player
        }
    }

    public override void ReInit(Vector3 position)
    {
        base.ReInit(position);
        isPlayerOn = false;
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.tag == "Player")
        {
            isPlayerOn = true;
        }
    }

    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        if (collision.transform.tag == "Player")
        {
            isPlayerOn = false;
        }
    }
}
