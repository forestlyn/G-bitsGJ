using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public enum InputType
{
    None,
    SwipeLeft,
    SwipeRight,
    SwipeDown,
    Delete
}
public class InputManager
{
    private Vector2 startTouchPosition;
    private Vector2 currentTouchPosition;
    private bool isSwiping = false;
    GameObject inputPrefab = Resources.Load<GameObject>("Input");

    GameObject inputObj = null;

    public InputManager()
    {

    }


    public void MyUpdate(float deltaTime)
    {
        if (deltaTime == 0)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
            Vector2 position = Camera.main.ScreenToWorldPoint(startTouchPosition);
            if (position.x >= leftUp.x && position.x <= rightDown.x
                    && position.y <= leftUp.y && position.y >= rightDown.y)
            {
                inputObj = GameObject.Instantiate(inputPrefab, position, Quaternion.identity);
            }
        }

        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            currentTouchPosition = Input.mousePosition;
            Vector2 direction = currentTouchPosition - startTouchPosition;
            DetectSwipe(direction);
            isSwiping = false;
            GameObject.Destroy(inputObj);
        }

        if (Input.GetMouseButtonUp(1))
        {
            startTouchPosition = Input.mousePosition;
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(startTouchPosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (hit.collider != null)
            {
                Debug.Log("hit.collider.name: " + hit.collider.name);
                HandleInput(InputType.Delete, hit.collider);
            }
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

    Vector2 leftUp = new Vector2(-2.5f, 4f);
    Vector2 rightDown = new Vector2(2.5f, -4.5f);
    private void HandleInput(InputType inputType, Collider2D collider2D = null)
    {

        var position = Camera.main.ScreenToWorldPoint(startTouchPosition);

        if (position.x >= leftUp.x && position.x <= rightDown.x
            && position.y <= leftUp.y && position.y >= rightDown.y)
        {
            GameManager.Instance.HandleInput(inputType, position, collider2D);
        }
    }
}
