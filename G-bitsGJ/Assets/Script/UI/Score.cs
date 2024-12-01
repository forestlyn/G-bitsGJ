using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int score;
    private Text text;
    private void Awake()
    {
        score = 0;
    }
    public void SetScore(int score)
    {
        this.score = score;
        text.text = score.ToString();
    }

}
