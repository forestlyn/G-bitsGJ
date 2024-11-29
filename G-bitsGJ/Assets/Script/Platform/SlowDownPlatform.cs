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
            ContactPoint2D contact = collision.GetContact(0);
            if (contact.point.y > transform.position.y)
            {
                var player = collision.transform.GetComponent<IPlayer>();
                player.Speed *= slowDownRate;
            }
        }
    }
    protected override void OnCollisionExit2D(Collision2D collision)
    {
        base.OnCollisionExit2D(collision);
        if (collision.transform.tag == "Player")
        {
            var player = collision.transform.GetComponent<IPlayer>();
            player.Speed /= slowDownRate;
        }
    }
}
