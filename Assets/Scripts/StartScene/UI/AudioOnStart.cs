using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class AudioOnStart : MonoBehaviour
{
    private AudioSource m_Audio;
    private GameObject game_1;
    private GameObject game_2;
    private GameObject game_3;

    private Button nextAudio;

    private float audio_Value;
    int index;

    private void Start()
    {
        index = UnityEngine.Random.Range(0, 10) % 3;

        m_Audio = gameObject.GetComponent<AudioSource>();
        m_Audio.playOnAwake = true;

        nextAudio = GameObject.Find("AudioText_Start").GetComponent<Button>();
        game_1 = Resources.Load<GameObject>("Audio/Start_1");
        game_2 = Resources.Load<GameObject>("Audio/Start_2");
        game_3 = Resources.Load<GameObject>("Audio/Start_3");

        SetAudio();

        nextAudio.onClick.AddListener(PlayNext);
    }


    private void Update()
    {
        UpdateVloume();
    }


    private void SetAudio()
    {
        if (index % 3 == 0)
        {
            m_Audio.clip = game_1.GetComponent<AudioSource>().clip;
        }
        else if(index % 3 == 1)
        {
            m_Audio.clip = game_2.GetComponent<AudioSource>().clip;
        }
        else
        {
            m_Audio.clip = game_3.GetComponent<AudioSource>().clip;
        }
        m_Audio.Play();
    }


    private void UpdateVloume()
    {
        if (!m_Audio.isPlaying)
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


    private void PlayNext()
    {
        index++;
        SetAudio();
    }
}
