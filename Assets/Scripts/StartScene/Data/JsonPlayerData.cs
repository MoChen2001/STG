using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonPlayerData : MonoBehaviour
{
    private PlayerData data;
    private string path;

    // 用来给主界面确定是否更新金币的数据
    public bool ditry;

    public static JsonPlayerData Instance;
    private string context;


    private void Start()
    {
        Instance = this;
        ditry = false;
        path = Application.persistentDataPath + "/PlayerData.txt";
        context = "{ \"Index\":\"1\",\"Audio\":\"0\",\"StarNum\":\"50\",\"Control\":\"1\",\"SkillCD\":\"0\",\"SkillTime\":\"0\",\"HardDegree\":\"0\"}";
        InitData();
    }




    // 读取数据
    private void InitData()
    {
        if(!File.Exists(path))
        {
            File.WriteAllText(path, context);
        }
        StreamReader streamReader = new StreamReader(path);
        string str = streamReader.ReadToEnd();
        streamReader.Close();

        data = JsonMapper.ToObject<PlayerData>(str);
        UpdateSkilCD();
        UpdateJson();
    }


    private void UpdateSkilCD()
    {
        if(JsonShopData.Instance != null)
        {
            data.SkillCD = JsonShopData.Instance.GetSkillCD(data.Index);
            data.SkillTime= JsonShopData.Instance.GetSkillTime(data.Index);
        }
    }

    // 更新金币数,第二个参数为true时是加，否则是减
    public void UpdateStarNum(int num,bool flag = true)
    {
        ditry = true;
        if (flag)
        {
            data.StarNum = (int.Parse(data.StarNum) + num).ToString();
        }
        else
        {
            data.StarNum = (int.Parse(data.StarNum) - num).ToString();
        }
        UpdateJson();
    }


    // 更新音频设置
    public void UpdateAudio(float audio)
    {
        data.Audio = audio.ToString();
        UpdateJson();
    }


    // 更新操作设置
    public void UpdateControl()
    {
        if (data.Control == "0")
        {
            data.Control = "1";
        }
        else
        {
            data.Control = "0";
        }
        UpdateJson();
    }


    // 更新当前使用机型
    public void UpdateIndex(string index)
    {
        data.Index = index;
        UpdateSkilCD();
        UpdateJson();
    }

    public void UpdateHardDegre(int index)
    {
        data.HardDegree = index.ToString();
        UpdateJson();
    }







    // 返回当前使用机型
    public string GetDataIndex()
    {
        return data.Index;
    }

    // 返回金币个数
    public string GetDataStarNum()
    {
        return data.StarNum;
    }


    // 返回音频设置
    public string GetDataAudio()
    {
        return data.Audio;
    }


    // 返回操作设置
    public string GetDataControl()
    {
        return data.Control;
    }

    // 返回CD值 
    public string GetDataSkillCD()
    {
        return data.SkillCD;
    }

    // 返回技能持续时间
    public string GetDataSkillTime()
    {
        return data.SkillTime;
    }

    // 返回游戏难度
    public string GetDataHardDegree()
    {
        return data.HardDegree;
    }



    // 更新Json文件
    private void UpdateJson()
    {
        FileStream file = new FileStream(path, FileMode.OpenOrCreate);
        file.SetLength(0);
        file.Close();

        StreamWriter streamWriter = new StreamWriter(path);
        string str = JsonMapper.ToJson(data);
        streamWriter.Write(str);
        streamWriter.Close();
    }



}
