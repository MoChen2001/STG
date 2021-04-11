using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIGaming : MonoBehaviour
{
    private Button button_Parse;

    private ETCButton button_Attack;
    private ETCJoystick etcJoystick;
    private ETCTouchPad etcTouchPad;


    private Text starText;


    private string index;
    private string control;


    private void Start()
    {
        InitOject();
    }


    private void InitOject()
    {

        index = JsonPlayerData.Instance.GetDataIndex();
        control = JsonPlayerData.Instance.GetDataControl();

        etcJoystick = GameObject.Find("Joystick").GetComponent<ETCJoystick>();
        etcTouchPad = GameObject.Find("TouchPad").GetComponent<ETCTouchPad>();


        button_Attack = GameObject.Find("Attack").GetComponent<ETCButton>();
        button_Parse = GameObject.Find("GamingParseButton").GetComponent<Button>();

        starText = GameObject.Find("StarNumText").GetComponent<Text>();

        button_Parse.onClick.AddListener(UIStateController.Instance.ParseState);

        button_Attack.onDown.AddListener(PlayerController.Instance.PlayerAttack);
        button_Attack.onUp.AddListener(PlayerController.Instance.PlayerAttackEnd);


        etcJoystick.onMoveSpeed.AddListener(PlayerController.Instance.PlayerMove);
        etcTouchPad.onMoveSpeed.AddListener(PlayerController.Instance.PlayerMove);

        if (control == "0")
        {
            etcJoystick.gameObject.SetActive(false);
        }
        else
        {
            etcTouchPad.gameObject.SetActive(false);
        }

        string path = "Player/ship_" + index;
        PlayerController.Instance.InitPlayer(path);
    }


    private void Update()
    {
        UpdateCoontrol();
        if (etcTouchPad.gameObject.active == true)
        {
            etcTouchPad.GetComponent<Image>().color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        }
        starText.text = UIStateController.Instance.GetStarNum().ToString();
    }

    private void UpdateCoontrol()
    {
        if (UIStateController.Instance.controlDirty)
        {
            control = JsonPlayerData.Instance.GetDataControl();
            if (control == "0")
            {
                etcJoystick.gameObject.SetActive(false);
                etcTouchPad.gameObject.SetActive(true);
            }
            else
            {
                etcTouchPad.gameObject.SetActive(false);
                etcJoystick.gameObject.SetActive(true);
            }
        }
    }
}
