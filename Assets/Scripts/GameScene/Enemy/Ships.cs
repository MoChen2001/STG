using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ships : MonoBehaviour
{

    private GameObject bullet_Common;
    private GameObject bullet_Big;
    private GameObject bullets;


    private int baseNumber;
    private int baseSpeed;


    public GameObject Bullet_Common
    {
        get 
        {
            if(bullet_Common == null)
            {
                bullet_Common = Resources.Load<GameObject>("Bullet/Bullet_Cycle_Common");
            }
            return bullet_Common;
        }
    }
    public GameObject Bullet_Big
    {
        get 
        {
            if (bullet_Big == null)
            {
                bullet_Big = Resources.Load<GameObject>("Bullet/Bullet_Cycle_Big");
            }
            return bullet_Big;
        }
    }
    public GameObject Bullets
    {
        get 
        { 
            if(bullets == null)
            {
                bullets = GameObject.Find("BulletParent");
            }
            return bullets; 
        }
    }

    public int BaseNumber
    {
        get 
        { 
            if(baseNumber == 0)
            {
                baseNumber = GameData.Instance.BulletNumber;
            }
            return baseNumber;
        }
    }

    public int BaseSpeed
    {
        get 
        {
            if (baseSpeed == 0)
            {
                baseSpeed = GameData.Instance.Speed;
            }
            return baseSpeed; 
        }
    }



    private void Awake()
    {


        bullet_Common = Resources.Load<GameObject>("Bullet/Bullet_Cycle_Common");
        bullet_Big = Resources.Load<GameObject>("Bullet/Bullet_Cycle_Big");
        bullets = GameObject.Find("BulletParent");


        baseNumber = GameData.Instance.BulletNumber;
        baseSpeed = GameData.Instance.Speed;
    }



    virtual public void MinusLife()
    {
        Debug.Log("wrong");
    }



}
