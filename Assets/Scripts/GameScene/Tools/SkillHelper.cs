using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SkillHelper : MonoBehaviour
{
    public static SkillHelper Instance;
    private string index;
    private float skillTime;
    private float skillCD;
    private bool dirty;


    private GameObject bullet_other;
    private GameObject bullet;
    private GameObject bullets;

    private GameObject friendShip;

    private void Awake()
    {
        Instance = this;
        dirty = false;
        friendShip = Resources.Load<GameObject>("FriendFire/Ship_1");
        bullet_other = Resources.Load<GameObject>("Bullet/Bullet_Other");
        bullets = GameObject.Find("BulletParent");

        //Y 65   -57
        //X -300 -200
    }



    // 使用技能的方法，根据索引调用相应函数
    public void UseSkill(GameObject obj)
    {
        index = JsonPlayerData.Instance.GetDataIndex();
        bullet = Resources.Load<GameObject>("Bullet/Bullet_Player_" + index);
        skillTime = (float)Convert.ToInt32(JsonPlayerData.Instance.GetDataSkillTime());
        skillCD = (float)Convert.ToInt32(JsonPlayerData.Instance.GetDataSkillCD());
        if (!dirty)
        {
            dirty = true;
            StartCoroutine(SkillTimeClac(skillCD));
            if (index == "1")
            {
                SkillShip_1();
            }
            else if (index == "2")
            {
                StartCoroutine(SkillShip_2(obj, skillTime));
            }
            else if (index == "3")
            {
                StartCoroutine(SkillShip_3(obj, skillTime));
            }
            else if(index == "4")
            {
                StartCoroutine(SkillShip_4(obj, skillTime));
            }
        }

    }

    private IEnumerator SkillTimeClac(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        dirty = false;
    }


    // 飞船一的技能。实现主要是在 FriendFire类中
    private void SkillShip_1()
    {
        Quaternion rot = Quaternion.Euler(new Vector3(0.0f, 90f, -90f));
        GameObject.Instantiate(friendShip, new Vector3(-300, 65, 260), rot);
        GameObject.Instantiate(friendShip, new Vector3(-300, -57, 260), rot);
    }



    private IEnumerator SkillShip_2(GameObject obj,float shotTime)
    {
        BulletShotHelper m_shot = obj.GetComponent<BulletShotHelper>();
        int index = m_shot.StartMoreRotationLines(bullet,bullets,12,20,50,0.1f);
        yield return new WaitForSeconds(shotTime);
        m_shot.StopCoroutineWithIndex(index);
    }





    private IEnumerator SkillShip_3(GameObject obj, float shotTime)
    {
        BulletShotHelper m_shot = obj.GetComponent<BulletShotHelper>();
        ShipControl m_ShipControl = obj.GetComponent<ShipControl>();
        m_ShipControl.NotDieStateStart();
        int index = m_shot.StartCycleLines(bullet, bullets, 12, 10, false, 80, 0.1f);


        //m_shot.StartMoreLineWithTimeOffsetAndTrace(bullet, bullets, new Vector3(50, 0, 260), 3, 20, 30, 50, 0.1f, 0.5f);



        //int index = m_shot.StartCycleLinesWithTimeOffsetAndTrace(bullet, bullets, new Vector3(50, 0, 260f),
        //    38, 10, false, 30, 100, 0.1f, 1.0f);

        yield return new WaitForSeconds(shotTime);
        m_shot.StopCoroutineWithIndex(index);
        if(PlayerPrefs.GetInt("NotDie") == 0)
        {
            m_ShipControl.NotDieStateEnd();
        }
    }


    private IEnumerator SkillShip_4(GameObject obj, float shotTime)
    {
        BulletShotHelper m_shot = obj.GetComponent<BulletShotHelper>();
        ShipControl m_ShipControl = obj.GetComponent<ShipControl>();
        m_ShipControl.NotDieStateStart();
        int index = m_shot.StartCycleBoom(bullet, bullet, bullets,12,12,100,60,0.1f,0.5f);
        int index_2 = m_shot.StartRotationLines(bullet_other, bullets, 12, 10, 100, 0.1f);
        yield return new WaitForSeconds(shotTime);
        m_shot.StopCoroutineWithIndex(index);
        m_shot.StopCoroutineWithIndex(index_2);
        if (PlayerPrefs.GetInt("NotDie") == 0)
        {
            m_ShipControl.NotDieStateEnd();
        }
    }
}
