using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class ParseUI : MonoBehaviour
{

    private Slider slider_Audio;

    private Button button_Home;
    private Button button_Back;
    private Button buton_Control;

    private Image image_Control;

    private int control_Value;


    private void Start()
    {
        control_Value = Convert.ToInt32(JsonPlayerData.Instance.GetDataControl());


        slider_Audio = GameObject.Find("AudioSlider").GetComponent<Slider>();
        button_Home = GameObject.Find("ParseHome").GetComponent<Button>();
        button_Back = GameObject.Find("ParseBack").GetComponent<Button>();
        buton_Control = GameObject.Find("ParseControl").GetComponent<Button>();
        image_Control = GameObject.Find("ParseControl").GetComponent<Image>();

        button_Home.onClick.AddListener(() =>{ SceneManager.LoadScene("Start"); } );

        button_Back.onClick.AddListener(() =>{ UIStateController.Instance.GamingState(); });

        buton_Control.onClick.AddListener(UpdateControl);

        slider_Audio.onValueChanged.AddListener(AudioChanged);


        slider_Audio.value = (float)Convert.ToDouble(JsonPlayerData.Instance.GetDataAudio());
        image_Control.sprite = Resources.Load<GameObject>("UI/control_" + control_Value).GetComponent<Image>().sprite;
    }



    private void UpdateControl()
    {
        UIStateController.Instance.controlDirty = true;
        if(JsonPlayerData.Instance.GetDataControl() == "0")
        {
            control_Value = 1;
        }
        else
        {
            control_Value = 0;
        }
        image_Control.sprite = Resources.Load<GameObject>("UI/control_" + control_Value).GetComponent<Image>().sprite;
        JsonPlayerData.Instance.UpdateControl();
    }



    private void AudioChanged(float value)
    {
        JsonPlayerData.Instance.UpdateAudio(value);
    }

}
