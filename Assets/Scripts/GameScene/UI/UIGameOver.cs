using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    private Text starNumText;
    private Text timeNumText;

    private Button button_Home;
    private Button button_Retry;

    private bool dirty;


    private void Start()
    {
        InitObject();
    }


    private void Update()
    {
        SetTimeText();
    }

    private void InitObject()
    {

        dirty = false;

        starNumText = GameObject.Find("OverStarScoreText").GetComponent<Text>();
        timeNumText = GameObject.Find("OverTimeScoreText").GetComponent<Text>();

        button_Home = GameObject.Find("OverBackHomeButton").GetComponent<Button>();
        button_Retry = GameObject.Find("RetryButton").GetComponent<Button>();


        button_Home.onClick.AddListener(() => { SceneManager.LoadScene("Start"); });
        button_Retry.onClick.AddListener(() => { SceneManager.LoadScene("Game"); });
    }



    private void SetTimeText()
    {
        if(UIStateController.Instance.gameOver)
        {
            int temp = UIStateController.Instance.GetStarNum();
            starNumText.text = temp.ToString() ;
            timeNumText.text = string.Format(" {0:f2} s",Time.time);
            if(!dirty)
            {
                dirty = true;
                JsonPlayerData.Instance.UpdateStarNum(temp);
            }
        }
    }
}
