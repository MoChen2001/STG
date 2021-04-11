using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{


    private GameObject panel_Start;
    private GameObject panel_Shop;
    private GameObject panel_Setting;

    public static StateController Instance;

    public void Awake()
    {

        Instance = this;

        panel_Start = GameObject.Find("StartPanel");
        panel_Shop = GameObject.Find("ShopPanel");
        panel_Setting = GameObject.Find("SettingPanel");


        StartState();

        PlayerPrefs.SetInt("NotDie",0);
    }



    // 设置开始状态的函数
    public void StartState()
    {
        panel_Start.SetActive(true);
        panel_Setting.SetActive(false);
        panel_Shop.SetActive(false);
    }

    public void ShopState()
    {
        panel_Start.SetActive(false);
        panel_Setting.SetActive(false);
        panel_Shop.SetActive(true);
    }


    public void SettingState()
    {
        panel_Start.SetActive(false);
        panel_Setting.SetActive(true);
        panel_Shop.SetActive(false);
    }
}
