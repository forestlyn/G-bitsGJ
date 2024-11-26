using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinePlatform : BasePlatform
{
    public bool isPlayerOn = false;
    public override void MyFixedUpdate(float fixedDeltaTime)
    {
        base.MyFixedUpdate(fixedDeltaTime);
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            isPlayerOn = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            isPlayerOn = false;
        }
    }
}
