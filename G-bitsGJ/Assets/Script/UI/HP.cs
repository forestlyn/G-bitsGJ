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
        float maxHP = 20;
        image.fillAmount = hp / maxHP;
    }
}
