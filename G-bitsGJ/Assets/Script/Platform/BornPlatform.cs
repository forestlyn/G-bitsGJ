using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BornPlatform : BasePlatform
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void MyUpdate(float deltaTime)
    {
        base.MyUpdate(deltaTime);
        animator.SetBool("IsPlaying", true);
    }
}
