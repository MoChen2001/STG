using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class AudioOnGaming : MonoBehaviour
{
    private AudioSource m_Audio;
    private GameObject game_1;
    private GameObject game_2;

    private float audio_Value;
    int index;

    private void Start()
    {
        index = UnityEngine.Random.Range(0, 10) % 2;

        audio_Value = (float)Convert.ToDouble(JsonPlayerData.Instance.GetDataAudio());
        m_Audio = gameObject.GetComponent<AudioSource>();
        m_Audio.playOnAwake = true;

        game_1 = Resources.Load<GameObject>("Audio/Game_1");
        game_2 = Resources.Load<GameObject>("Audio/Game_2");


        SetAudio();

    }


    private void Update()
    {
        UpdateVloume();
        m_Audio.volume = audio_Value;
    }


    private void SetAudio()
    {
        if(index % 2  == 0)
        {
            m_Audio.clip = game_1.GetComponent<AudioSource>().clip;
        }
        else
        {
            m_Audio.clip = game_2.GetComponent<AudioSource>().clip;
        }
        m_Audio.Play();
    }


    private void UpdateVloume()
    { 
        if(!m_Audio.isPlaying)
        {
            index++;
            SetAudio();
        }
        if (audio_Value != (float)Convert.ToDouble(JsonPlayerData.Instance.GetDataAudio()))
        {
            audio_Value = (float)Convert.ToDouble(JsonPlayerData.Instance.GetDataAudio());
            m_Audio.volume = audio_Value;
        }
    }
    

    public void SetMusic(int x)
    {
        index = x % 2;
        SetAudio();
    }
}
