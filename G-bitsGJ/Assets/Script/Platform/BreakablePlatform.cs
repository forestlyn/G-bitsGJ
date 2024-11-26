using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : BasePlatform
{
    public float breakTime = 2f;

    [SerializeField]
    private bool beginTimer = false;
    [SerializeField]
    private float timer = 0;
    public override void MyFixedUpdate(float fixedDeltaTime)
    {
        base.MyFixedUpdate(fixedDeltaTime);
        if(beginTimer)
        {
            timer += fixedDeltaTime;
            if (timer >= breakTime)
            {
                Reduce();
            }
        }

    }


    public override void ReInit(Vector3 position)
    {
        base.ReInit(position);
        beginTimer = false;
        timer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            beginTimer = true;
        }
    }
}
