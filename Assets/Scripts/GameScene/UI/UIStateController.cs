using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIStateController : MonoBehaviour
{
    private GameObject gamingPanel;
    private GameObject gameOverPanel;
    private GameObject parsePanel;
    private GameObject GamePass;


    private int starNum;
    public bool gameOver;
    public bool gameParse;
    public bool gamePass;
    public bool controlDirty;

    public static UIStateController Instance;

    private void Awake()
    {
        Instance = this;
        gameOver = false;
        gameParse = false;
        controlDirty = false;
        starNum = 0;
        gamingPanel = GameObject.Find("GamingPanel");
        gameOverPanel = GameObject.Find("GameOverPanel");
        parsePanel = GameObject.Find("ParsePanel");
        GamePass = GameObject.Find("GamePass");
        GamingState();
    }


    // 改变状态为游戏状态
    public void GamingState()
    {
        Time.timeScale = 1.0f;
        gameParse = false;
        gameOver = false;
        gamingPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        parsePanel.SetActive(false);
        GamePass.SetActive(false);
    }

    // 改变状态为游戏结束状态
    public void GameOverState()
    {
       
        gameOver = true;
        Time.timeScale = 0.0f;
        gamingPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        parsePanel.SetActive(false);
        GamePass.SetActive(false);
    }

    // 改变状态为暂停状态
    public void ParseState()
    {
        gameParse = true;
        Time.timeScale = 0.0f;
        gamingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        parsePanel.SetActive(true);
        GamePass.SetActive(false);
    }


    // 改变状态为通关状态
    public void PassState()
    {
        Time.timeScale = 0.0f;
        gamingPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        parsePanel.SetActive(false);
        GamePass.SetActive(true);
    }

    // 对金币进行增加
    public void AddNum()
    {
        starNum++;
    }


    // 返回金币数
    public int GetStarNum()
    {
        return starNum;
    }
}
