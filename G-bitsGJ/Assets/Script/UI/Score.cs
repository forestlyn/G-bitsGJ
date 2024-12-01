using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    private Text text;
    private string preStr;
    private void Awake()
    {
        preStr = "梦境时长：";
        score = 0;
        text = GetComponent<Text>();
    }
    public void SetScore(int score)
    {
        this.score = score;
        text.text = preStr + score.ToString();
    }

}
