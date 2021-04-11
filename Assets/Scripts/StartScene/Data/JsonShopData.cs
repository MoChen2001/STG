using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using LitJson;


public class JsonShopData : MonoBehaviour
{
    private List<ShipInfo> shipLists;

    private string writePath;
    private string context = @"[
	{
		""Index"":""1"",
		""Price"":""10"",
		""CanUse"":""1"",
		""SkillName"":""替身攻击！"",
		""SkillInfo"":""召唤出两台普通的机型，对前方的锥型区域机型进行持续五秒范围扫射"",
		""Image"":""Skill_Ship_1"",
		""SkillCD"":""10"",
		""SkillTime"":""5""

    },
	{
        ""Index"":""2"",
		""Price"":""10"",
		""CanUse"":""0"",	
		""SkillName"":""多重射击"",
		""SkillInfo"":""自身进行单行螺旋射击,持续八秒"",
		""Image"":""Skill_Ship_2"",
		""SkillCD"":""20"",
		""SkillTime"":""8""

    },
	{
        ""Index"":""3"",
		""Price"":""10"",
		""CanUse"":""0"",
		""SkillName"":""欧啦欧啦！"",
		""SkillInfo"":""自身进入无敌状态，同时进行螺旋扫射，持续十秒"",
		""Image"":""Skill_Ship_3"",
		""SkillCD"":""30"",
		""SkillTime"":""10""

    },
	{
        ""Index"":""4"",
		""Price"":""10"",
		""CanUse"":""0"",
		""SkillName"":""木大木大！"",
		""SkillInfo"":""自身进入无敌状态，同时进行圆环封锁型射击，持续十二秒。"",
		""Image"":""Skill_Ship_4"",
		""SkillCD"":""40"",
		""SkillTime"":""12""

    }
]";

    public static JsonShopData Instance;

    private void Awake()
    {
        Instance = this;
        writePath = Application.persistentDataPath + "/ShopData.txt";
        shipLists = new List<ShipInfo>();

        InitList();

    }


    // 初始化数据
    private void InitList()
    {
        if (!File.Exists(writePath))
        {
            File.WriteAllText(writePath, context);
        }

        // Resources 会出错，直接使用文件读取流进行处理
        //TextAsset textAsset = Resources.Load<TextAsset>(readPath);
        //string str = textAsset.text;



        StreamReader streamReader = new StreamReader(writePath);
        string str = streamReader.ReadToEnd();
        streamReader.Close();


        JsonData jsonData = JsonMapper.ToObject(str);
        for(int i = 0; i < jsonData.Count; i++)
        {
            shipLists.Add(JsonMapper.ToObject<ShipInfo>(jsonData[i].ToJson()));
        }


        //foreach(ShipInfo x in shipLists)
        //{
        //    Debug.Log(x);
        //}
    }


    // 更新数据
    private void UpdateJson()
    {

        FileStream file = new FileStream(writePath, FileMode.OpenOrCreate);
        file.SetLength(0);
        file.Close();


        string str = JsonMapper.ToJson(shipLists);
        StreamWriter jsonWriter = new StreamWriter(writePath);


        jsonWriter.Flush();
        jsonWriter.Write(str);
        jsonWriter.Close();


    }


    // 给购买留一个接口
    public void UpdateCanUse(string index)
    {
        foreach(ShipInfo x in shipLists)
        {
            if(x.Index == index)
            {
                x.CanUse = "1";
                break;
            }
        }
        UpdateJson();
    }


    // 读取技能名称
    public string GetSkillName(string index)
    {
        foreach(ShipInfo x in shipLists)
        {
            if(x.Index == index)
            {
                return x.SkillName;
            }
        }
        return null;
    }


    // 读取技能信息
    public string GetSkillInfo(string index)
    {
        foreach (ShipInfo x in shipLists)
        {
            if (x.Index == index)
            {
                return x.SkillInfo;
            }
        }
        return null;
    }

    // 读取商品价格
    public string GetPrice(string index)
    {
        foreach (ShipInfo x in shipLists)
        {
            if (x.Index == index)
            {
                return x.Price;
            }
        }
        return null;
    }

    // 读取是否可用
    public string GetCanUse(string index)
    {
        foreach (ShipInfo x in shipLists)
        {
            if (x.Index == index)
            {
                return x.CanUse;
            }
        }
        return null;
    }

    // 读取图片信息
    public string GetImage(string index)
    {
        foreach (ShipInfo x in shipLists)
        {
            if (x.Index == index)
            {
                return x.Image;
            }
        }
        return null;
    }

    public string GetSkillCD(string index)
    {
        foreach (ShipInfo x in shipLists)
        {
            if (x.Index == index)
            {
                return x.SkillCD;
            }
        }
        return null;
    }


    public string GetSkillTime(string index)
    {
        foreach (ShipInfo x in shipLists)
        {
            if (x.Index == index)
            {
                return x.SkillTime;
            }
        }
        return null;
    }
}
