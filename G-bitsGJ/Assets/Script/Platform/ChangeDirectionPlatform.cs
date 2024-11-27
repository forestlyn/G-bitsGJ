using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDirectionPlatform : BasePlatform
{
    [SerializeField]
    public PlayerDirection direction;

    public Sprite[] sprites;

    public SpriteRenderer spriteRenderer;
    public override void ReInit(Vector3 position)
    {
       base.ReInit(position);
        direction = Random.Range(0, 2) == 0 ? PlayerDirection.Left : PlayerDirection.Right;
        spriteRenderer.sprite = sprites[(int)direction];
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
