using System.Collections;
using System.Collections.Generic;

public class ShipInfo
{
    private string index;
    public string Index
    {
        get { return index; }
        set { index = value; }
    }

    private string price;
    public string Price
    {
        get { return price; }
        set { price = value; }
    }

    
    private string canUse;
    public string CanUse
    {
        get { return canUse; }
        set { canUse = value; }
    }

    private string skillName;
    public string SkillName
    {
        get { return skillName; }
        set { skillName = value; }
    }


    private string skillInfo;
    public string SkillInfo
    {
        get { return skillInfo; }
        set { skillInfo = value; }
    }

    private string image;
    public string Image
    {
        get { return image; }
        set { image = value; }
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

     
    public ShipInfo() { }
    public ShipInfo(string index,string price,string canUse,string skillName,string skillInfo,
        string image,string skillCD,string skillTime)
    {
        this.Index = index;
        this.Price = price;
        this.CanUse = canUse;
        this.SkillName = skillName;
        this.SkillInfo = skillInfo;
        this.Image = image;
        this.SkillCD = skillCD;
        this.SkillTime = skillTime;
    }



    override public string ToString()
    {
        return string.Format("{0},{1},{2},{3},{4},{5},{6},{7}", 
            Index, Price, canUse, SkillName, SkillInfo,Image,SkillCD, SkillTime);
    }
}
