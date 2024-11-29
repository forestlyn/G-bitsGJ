using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionPlatform : BasePlatform
{
    [SerializeField]
    public PlayerDirection direction;

    public override void ReInit(Vector3 position)
    {
        base.ReInit(position);
        direction = platformType == PlatformType.ChangeDirectionLeft ? PlayerDirection.Left : PlayerDirection.Right;    
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.tag == "Player")
        {
            // 改变Player方向

        }
    }
}
