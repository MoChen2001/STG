using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerData
{
    private string index;
    public string Index
    {
        get { return index; }
        set { index = value; }
    }


    private string audio;
    public string Audio
    {
        get { return audio; }
        set { audio = value; }
    }

    private string starNum;
    public string StarNum
    {
        get { return starNum; }
        set { starNum = value; }
    }


    private string control;
    public string Control
    {
        get { return control; }
        set { control = value; }
    }


    private string skillCD;
    public string SkillCD
    {
        get { return skillCD; }
        set { skillCD = value; }
    }


    private string skillTime;
    public string SkillTime
    {
        get { return skillTime; }
        set { skillTime = value; }
    }



    private string hardDegree;
    public string HardDegree
    {
        get { return hardDegree; }
        set { hardDegree = value; }
    }


    public PlayerData() { }

    public PlayerData(string audio,string index,string starNum,string control,string SkillCD,
        string skillTime,string hardDegree)
    {
        this.Audio = audio;
        this.Index = index;
        this.StarNum = starNum;
        this.Control = control;
        this.SkillCD = SkillCD;
        this.SkillTime = skillTime;
        this.HardDegree = hardDegree;
    }

    public override string ToString()
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{7}", audio, index, starNum, control, SkillCD,SkillTime,HardDegree);
    }
}
