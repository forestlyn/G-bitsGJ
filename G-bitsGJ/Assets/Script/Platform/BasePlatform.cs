using System;
using UnityEngine;

public class BasePlatform : MonoBehaviour
{
    public float speed = 1.0f;
    public PlatformType platformType;


    public virtual void Reduce()
    {
        gameObject.SetActive(false);
    }


    public virtual void ReInit(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public virtual void MyUpdate(float fixedDeltaTime)
    {
        // 计算物体在世界空间中的移动量
        Vector3 movement = speed * Vector3.up * fixedDeltaTime;
        //Debug.Log("movement: " + movement+" "+transform.position);
        // 将移动量应用到物体的位置上
        transform.position += (movement);
        if (movement.y >= 0)
        {
            //Debug.Log("movement: " + movement + " " + transform.position + " frament:" + framentCount + " " + Time.deltaTime);
            framentCount++;
        }
        if (transform.position.y > 6)
        {
            Reduce();
        }
    }

    public int framentCount = 0;
    //private void Update()
    //{
    //    // 计算物体在世界空间中的移动量
    //    Vector3 movement = speed * Vector3.up * Time.deltaTime;
    //    //Debug.Log("movement: " + movement+" "+transform.position);
    //    // 将移动量应用到物体的位置上
    //    transform.position += (movement);
    //    if (movement.y >= 0)
    //    {
    //        Debug.Log("movement: " + movement + " " + transform.position + " frament:" + framentCount + " " + Time.deltaTime);
    //        framentCount++;
    //    }
    //    if (transform.position.y > 6)
    //    {
    //        Reduce();
    //    }
    //}

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        //Player进入
        if (collision.transform.tag == "Player")
        {
            ContactPoint2D contact = collision.GetContact(0);

            if (contact.point.y > transform.position.y)
            {
                Debug.Log("Player is on top of the platform");
                IPlayer player = collision.transform.GetComponent<IPlayer>();
                if (player != null)
                {
                    player.Speed = 1f;
                    player.ChangeState(PlayerState.Walk);
                }
            }
        }
    }

    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        //player离开
        if (collision.transform.tag == "Player")
            collision.transform.GetComponent<Player>()?.ChangeState(PlayerState.Drop);
    }
}
