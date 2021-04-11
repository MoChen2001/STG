using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public static GameData Instance;

    private int hardDegree;
    public int HardDegree
    {
        get { return hardDegree; }
    }


    private float maxRange;
    public float MaxRange
    {
        get { return maxAngle; }
    }


    private float maxAngle;
    public float MaxAngle
    {
        get { return maxAngle; }
    }

    private int maxShotNumber;
    public int MaxShotNumber
    {
        get { return maxShotNumber; }
    }


    private int life;
    public int Life
    {
        get { return life; }
    }



    private int speed;
    public int Speed
    {
        get { return speed; }
    }

    private int bulletNumber;
    public int BulletNumber
    {
        get { return bulletNumber; }
    }


    private void Start()
    {
        Instance = this;
        hardDegree = int.Parse(JsonPlayerData.Instance.GetDataHardDegree());
        InitValue();
    }



    private int baseEnemyNumber;
    public int BaseEnemyNuber
    {
        get { return baseEnemyNumber; }
    }



    private void InitValue()
    {
        if(hardDegree == 0)
        {
            life = 8;
            maxShotNumber = 8;
            maxRange = 40;
            maxAngle = 30;
            bulletNumber = 1;
            speed = 100;
            baseEnemyNumber = 3;
        }
        else if(hardDegree == 1)
        {
            life = 6;
            maxShotNumber = 12;
            maxRange = 60;
            maxAngle = 60;
            bulletNumber = 3;
            speed = 100;
            baseEnemyNumber = 6;
        }
        else if (hardDegree == 2)
        {
            life = 4;
            maxShotNumber = 16;
            maxRange = 80;
            maxAngle = 90;
            bulletNumber = 5;
            speed = 150;
            baseEnemyNumber = 9;
        }
        else
        {
            life = 2;
            maxShotNumber = 100000;
            maxRange = 10000;
            maxAngle = 180;
            bulletNumber = 8;
            speed = 200;
            baseEnemyNumber = 12;
        }
    }

}
