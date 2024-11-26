using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionPlatform : BasePlatform
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            // 改变Player方向
        }
    }
}
