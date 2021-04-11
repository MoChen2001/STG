using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGamePass : MonoBehaviour
{
    private Text starNumText;
    private Text timeNumText;

    private Button button_Home;



    private void Start()
    {

        starNumText = GameObject.Find("PassStarScoreText").GetComponent<Text>();
        timeNumText = GameObject.Find("PassTimeScoreText").GetComponent<Text>();

        button_Home = GameObject.Find("PassBackHomeButton").GetComponent<Button>();


        button_Home.onClick.AddListener(() => { SceneManager.LoadScene("Start"); });

        int temp = UIStateController.Instance.GetStarNum();
        starNumText.text = temp.ToString();
        timeNumText.text = string.Format(" {0:f2} s", Time.time);
    }



}
