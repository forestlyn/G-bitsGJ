using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour , IUIManager
{
    private static UIManager instance;
    public static IUIManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new UIManager();
            }
            return instance;
        }
    }

    public GameObject startMenu;
    public GameObject gameUI;
    public GameObject gameOver;
    public GameObject pause;
    public GameObject win;

    public HP hpManager;
    public Score scoreManager;


    void IUIManager.Init()
    {

    }
    void IUIManager.ShowStartMenu(bool isShow)
    {
        startMenu.SetActive(isShow);
    }
    void IUIManager.ShowGameUI(bool isShow)
    {
        gameUI.SetActive(isShow);
    }
    void IUIManager.ShowGameOver(bool isShow)
    {
        gameOver.SetActive(isShow);
    }
    void IUIManager.ShowPause(bool isShow)
    {
        pause.SetActive(isShow);
    }
    void IUIManager.ShowWin(bool isShow)
    {
        win.SetActive(isShow);
    }
    void IUIManager.SetHP(int hp)
    {
        hpManager.SetHP(hp);
    }
    void IUIManager.SetScore(int score)
    {
        scoreManager.SetScore(score);
    }


}

public interface IUIManager
{
    void Init();
    void ShowStartMenu(bool isShow);
    void ShowGameUI(bool isShow);
    void ShowGameOver(bool isShow);
    void ShowPause(bool isShow);
    void ShowWin(bool isShow);
    void SetHP(int hp);
    void SetScore(int score);
}
