using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum InputType
{
    None,
    SwipeLeft,
    SwipeRight,
    SwipeDown
}
public class InputManager
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isSwiping = false;

    public InputManager()
    {
    }

    public void MyUpdate(float deltaTime)
    {
        if(deltaTime == 0)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            currentTouchPosition = Input.mousePosition;
            Vector2 direction = currentTouchPosition - startTouchPosition;
            DetectSwipe(direction);
            isSwiping = false;
        }
    }

    private void DetectSwipe(Vector2 direction)
    {
        if (direction.magnitude < 50) // 滑动距离阈值
        {
            return;
        }

        direction.Normalize();

        InputType inputType = InputType.None;

        if (Vector2.Dot(direction, Vector2.left) > 0.7f)
        {
            //Debug.Log("Swipe Left");
            // 处理左滑逻辑
            inputType = InputType.SwipeLeft;
        }
        else if (Vector2.Dot(direction, Vector2.right) > 0.7f)
        {
            //Debug.Log("Swipe Right");
            // 处理右滑逻辑
            inputType = InputType.SwipeRight;
        }
        else if (Vector2.Dot(direction, Vector2.down) > 0.7f)
        {
            //Debug.Log("Swipe Down");
            // 处理下滑逻辑
            inputType = InputType.SwipeDown;
        }

        HandleInput(inputType);
    }


    private void HandleInput(InputType inputType)
    {
        var position = Camera.main.ScreenToWorldPoint(startTouchPosition);
        GameManager.Instance.HandleInput(inputType, position);
    }
}
