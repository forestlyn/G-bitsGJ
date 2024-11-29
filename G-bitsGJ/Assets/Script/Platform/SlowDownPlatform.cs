using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPlatform : BasePlatform
{
    public float slowDownRate = 0.5f;
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.transform.tag == "Player")
        {
            // 获取碰撞点
            ContactPoint2D contact = collision.GetContact(0);

            // 检查碰撞点是否在平台上方
            if (contact.point.y > transform.position.y)
            {
                // 玩家在平台上方
                Debug.Log("Player is on top of the platform");
                // 在这里实现减速逻辑

            }
        }
    }
}
