using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{

    private Button button_Back;
    private Button button_Audio;
    private Button button_Control;

    private Image image_Audio;
    private Image image_Control;

    private Slider slider_Audio;


    private Dropdown dropDown_Hard;

    private int control_Value;


    private void Start()
    {
        InitObject();
    }


    private void Update()
    {
        JsonPlayerData.Instance.UpdateAudio(slider_Audio.value);
    }

    // 初始化和绑定事件的函数
    private void InitObject()
    {
        control_Value = 0;

        slider_Audio = GameObject.Find("AudioText").GetComponent<Slider>();
        image_Audio = GameObject.Find("AudioButton").GetComponent<Image>();
        image_Control = GameObject.Find("ControlButton").GetComponent<Image>();

        button_Back = GameObject.Find("SetBack").GetComponent<Button>();
        button_Audio = GameObject.Find("AudioButton").GetComponent<Button>();
        button_Control = GameObject.Find("ControlButton").GetComponent<Button>();

        dropDown_Hard = GameObject.Find("HardDropdown").GetComponent<Dropdown>();


        button_Back.onClick.AddListener(StateController.Instance.StartState);
        button_Audio.onClick.AddListener(AudioEvent);
        button_Control.onClick.AddListener(Control);
        dropDown_Hard.onValueChanged.AddListener(HardValueChanged);
        slider_Audio.onValueChanged.AddListener(AudioChanged);


        slider_Audio.value = float.Parse(JsonPlayerData.Instance.GetDataAudio());

        if(slider_Audio.value == 0.0f)
        {
            image_Audio.sprite = Resources.Load<GameObject>("UI/audio_1").GetComponent<Image>().sprite;
        }


        dropDown_Hard.value = int.Parse(JsonPlayerData.Instance.GetDataHardDegree());
    }

    private void AudioChanged(float value)
    {
        if(value == 0)
        {
            image_Audio.sprite = Resources.Load<GameObject>("UI/audio_1").GetComponent<Image>().sprite;
        }
        else
        {
            image_Audio.sprite = Resources.Load<GameObject>("UI/audio_0").GetComponent<Image>().sprite;
        }
        JsonPlayerData.Instance.UpdateAudio(value);
    }

    private void HardValueChanged(int x)
    {
        dropDown_Hard.value = x;
        JsonPlayerData.Instance.UpdateHardDegre(x);
    }


    // 点击更换音量事件
    private void AudioEvent()
    {
        if(slider_Audio.value == 0.0f)
        {
            slider_Audio.value = 1.0f;
            image_Audio.sprite = Resources.Load<GameObject>("UI/audio_0").GetComponent<Image>().sprite;
        }
        else
        {
            slider_Audio.value = 0.0f;
            image_Audio.sprite = Resources.Load<GameObject>("UI/audio_1").GetComponent<Image>().sprite;
        }
    }


    // 点击更换控制方式事件
    private void Control()
    {
        control_Value = (control_Value + 1) % 2;
        image_Control.sprite = Resources.Load<Image>("UI/control_" + control_Value).sprite;
        JsonPlayerData.Instance.UpdateControl();
    }
}
