using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void SetHP(int hp)
    {
        int maxHP = 100;
        image.fillAmount = hp / maxHP;
    }
}
