using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinePlatform : BasePlatform
{
    public bool isPlayerOn = false;

    public int Attack = 2;

    private IPlayer player = null;
    public override void MyUpdate(float deltaTime)
    {
        base.MyUpdate(deltaTime);
        if (isPlayerOn)
        {
            // 攻击player
            if (player != null)
                player.HP = player.HP - Attack;
            Debug.Log("Player HP: " + player.HP + gameObject.name);
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
            player = collision.transform.GetComponent<IPlayer>();
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
