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
            // ��ȡ��ײ��
            ContactPoint2D contact = collision.GetContact(0);

            // �����ײ���Ƿ���ƽ̨�Ϸ�
            if (contact.point.y > transform.position.y)
            {
                // �����ƽ̨�Ϸ�
                Debug.Log("Player is on top of the platform");
                // ������ʵ�ּ����߼�

            }
        }
    }
}
