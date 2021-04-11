using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartPanel : MonoBehaviour
{
    private Button button_Start;
    private Button button_Shop;
    private Button button_Setting;
    private Button button_Help;
    private Button button_NoDie;
    private Button button_Back;
    private Text text_StarNum;
    private Text text_Nodie;

    private int index = 0;

    private GameObject helpPanel;

    private void Start()
    {
        InitObject();
    }


    // 初始化所有变量以及对按钮进行事件绑定的函数
    private void InitObject()
    {
        button_Help = GameObject.Find("Help").GetComponent<Button>();
        button_Start = GameObject.Find("Start").GetComponent<Button>();
        button_Shop = GameObject.Find("Shop").GetComponent<Button>();
        button_Setting = GameObject.Find("Setting").GetComponent<Button>();
        button_NoDie = GameObject.Find("NoDie").GetComponent<Button>();
        button_Back = GameObject.Find("HelpBack").GetComponent<Button>();
        helpPanel = GameObject.Find("HelpPanel");

        text_Nodie = GameObject.Find("NoDie").GetComponent<Text>();
        text_StarNum = GameObject.Find("StarNum").GetComponent<Text>();
        PlayerPrefs.SetInt("NotDie", index);

        button_Start.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        button_Shop.onClick.AddListener(StateController.Instance.ShopState);
        button_Setting.onClick.AddListener(StateController.Instance.SettingState);

        button_NoDie.onClick.AddListener(() =>
        {
            if(index % 2 == 0)
            {
                text_Nodie.text = "关闭无敌";
                PlayerPrefs.SetInt("NotDie", 1);
                index = 1;
            }
            else
            {
                text_Nodie.text = "开启无敌";
                PlayerPrefs.SetInt("NotDie", 0);
                index = 0;
            }
        });

        button_Help.onClick.AddListener(() =>
        {
            helpPanel.SetActive(true);
            button_Help.enabled = false;
        });


        button_Back.onClick.AddListener(() =>
        {
            helpPanel.SetActive(false) ;
            button_Help.enabled = true;
        });

        helpPanel.SetActive(false);
        text_StarNum.text = JsonPlayerData.Instance.GetDataStarNum();
    }

    private void Update()
    {
        UpdateStar();
    }


    // 更新金币
    private void UpdateStar()
    {
        if (JsonPlayerData.Instance.ditry)
        {
            text_StarNum.text = JsonPlayerData.Instance.GetDataStarNum();
        }
        if(text_Nodie.gameObject.activeSelf)
        {
            if (PlayerPrefs.GetInt("NotDie") == 0)
            {
                text_Nodie.text = "开启无敌";
            }
            else
            {
                text_Nodie.text = "关闭无敌";
            }
        }
    }

}
