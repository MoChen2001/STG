using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPanel : MonoBehaviour
{
    private GameObject priceShow;
    private GameObject playerUI;
    private GameObject bugFailedPanel;

    private Image image_Skill;

    private Button button_Back;
    private Button button_Left;
    private Button button_Right;
    private Button button_ButtomUI;
    private Button button_BuyFailed;


    private Text text_ButtomUI;
    private Text text_SkillName;
    private Text text_SkillInfo;
    private Text text_Price;



    private int currentIndex;
    private int playerIndex;


    private void Start()
    {

        InitObject();
        InitShow();
    }



    // 初始化和绑定事件的函数
    private void InitObject()
    {
        currentIndex = 1;
        playerIndex = int.Parse(JsonPlayerData.Instance.GetDataIndex());

        priceShow = GameObject.Find("PriceImage");
        bugFailedPanel = GameObject.Find("BuyFailedPanel");


        image_Skill = GameObject.Find("Skillimage").GetComponent<Image>();

        button_Back = GameObject.Find("ShopBack").GetComponent<Button>();
        button_Left = GameObject.Find("Left").GetComponent<Button>();
        button_Right = GameObject.Find("Right").GetComponent<Button>();
        button_ButtomUI = GameObject.Find("ButtomUI").GetComponent<Button>();
        button_BuyFailed = GameObject.Find("BuyFailedButon").GetComponent<Button>();


        text_ButtomUI =  GameObject.Find("ButtomText").GetComponent<Text>();
        text_SkillName =  GameObject.Find("SkillName").GetComponent<Text>();
        text_SkillInfo =  GameObject.Find("SkillInfoText").GetComponent<Text>();
        text_Price = GameObject.Find("Price").GetComponent<Text>();

        button_Left.onClick.AddListener(LeftClick);
        button_Right.onClick.AddListener(RightClick);
        button_Back.onClick.AddListener(BackButtonClick);
        button_BuyFailed.onClick.AddListener(ReturnShop);


        LeftAndRightShow();

        bugFailedPanel.SetActive(false);
    }


    // 初始化界面
    private void InitShow()
    {
        ReInitUI();
        string skillName = JsonShopData.Instance.GetSkillName(currentIndex.ToString());
        string skillInfo = JsonShopData.Instance.GetSkillInfo(currentIndex.ToString());
        string price = JsonShopData.Instance.GetPrice(currentIndex.ToString());
        string canUse = JsonShopData.Instance.GetCanUse(currentIndex.ToString());
        string image = JsonShopData.Instance.GetImage(currentIndex.ToString());
        string skillCD = JsonShopData.Instance.GetSkillCD(currentIndex.ToString());

        text_SkillName.text = skillName;
        text_SkillInfo.text = skillInfo + "\n技能冷却时间：" + skillCD;

        GameObject temp = Resources.Load<GameObject>("PlayerUI/ship_"+currentIndex);
        playerUI = GameObject.Instantiate(temp, Vector3.zero, Quaternion.Euler(-90, 0.0f, 0.0f), gameObject.transform);

        playerUI.AddComponent<PlayerUI>();

        image_Skill.sprite = Resources.Load<GameObject>("UI/" + image).GetComponent<Image>().sprite;

        if(canUse == "1")
        {
            Initlock();
        }
        else
        {
            InitUnlock(price);
        }

    }


    // 返回按钮事件
    private void BackButtonClick()
    {
        StateController.Instance.StartState();
    }


    // 左点击按钮事件
    private void LeftClick()
    {
        currentIndex--;
        LeftAndRightShow();
        InitShow();
    }


    // 右点击按钮事件
    private void RightClick()
    {
        currentIndex++;
        LeftAndRightShow();
        InitShow();
    }

    // 左右按钮隐藏判断
    private void LeftAndRightShow()
    {
        if(currentIndex == 1)
        {
            button_Left.gameObject.SetActive(false);
        }
        else if(currentIndex == 4)
        {
            button_Right.gameObject.SetActive(false);
        }
        else
        {
            button_Left.gameObject.SetActive(true);
            button_Right.gameObject.SetActive(true);
        }
    }

    // 初始化未解锁物品
    private void InitUnlock(string price)
    {
        text_ButtomUI.text = "购买该机型";
        priceShow.SetActive(true);
        button_ButtomUI.enabled = true;
        text_Price.text = price;
        button_ButtomUI.onClick.AddListener(BuyShip);
    }


    // 初始化解锁物品
    private void Initlock()
    {
        priceShow.SetActive(false);
        if (playerIndex == currentIndex)
        {
            button_ButtomUI.enabled = false;
            text_ButtomUI.text = "当前使用机型";
        }
        else
        {
            button_ButtomUI.onClick.AddListener(SetPlayer);
            text_ButtomUI.text = "设为使用机型";
        }
    }


    // 设置皮肤事件
    private void SetPlayer()
    {
        playerIndex = currentIndex;
        InitShow();
        JsonPlayerData.Instance.UpdateIndex(playerIndex.ToString());
    }

    // 购买事件
    private void BuyShip()
    {
        int starNum = int.Parse(JsonPlayerData.Instance.GetDataStarNum());
        int price = int.Parse(JsonShopData.Instance.GetPrice(currentIndex.ToString()));
        if (starNum >= price)
        {
            JsonPlayerData.Instance.UpdateStarNum(price, false);
            JsonShopData.Instance.UpdateCanUse(currentIndex.ToString());
            InitShow();
        }
        else
        {
            bugFailedPanel.SetActive(true);
            playerUI.SetActive(false);
            button_Back.enabled = false;
            button_Left.enabled = false;
            button_Right.enabled = false;
        }
    }

    // 更新隐藏的UI
    private void ReInitUI()
    {

        priceShow.SetActive(true);
        button_ButtomUI.enabled = true;
        GameObject.Destroy(playerUI);
        playerUI = null;
        LeftAndRightShow();
    }


    // 购买失败返回商城界面事件
    private void ReturnShop()
    {
        bugFailedPanel.SetActive(false);
        button_Back.enabled = true;
        playerUI.SetActive(true);
        button_Left.enabled = true;
        button_Right.enabled = true;
    }
}
