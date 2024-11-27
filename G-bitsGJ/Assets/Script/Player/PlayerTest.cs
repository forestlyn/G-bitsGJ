using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    bool isUsed = false;
    void Update()
    {
        if(isUsed == false && Input.GetKeyDown(KeyCode.Space))
        {
            isUsed = true;
            PlayerManager.Instance.CreatePlayer(new Vector2(0, 5));
        }
    }
}
