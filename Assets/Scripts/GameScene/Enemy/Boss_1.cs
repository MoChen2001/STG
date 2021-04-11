using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_1 : MonoBehaviour
{
    // 通用变量
    private BulletShotHelper m_Shot;
    private EnemyActHelper m_Move;
    private Transform m_Transform;
    private Transform player_Transform;

    private AudioOnGaming audioSource;



    // 子弹相关
    private int baseNumber;
    private int baseSpeed;
    private GameObject bullets;
    private GameObject bullet_Common;
    private GameObject bullet_Big;

    private void Start()
    {
        Init();
    }

    /// <summary>
    /// 初始化各种成员
    /// </summary>
    private void Init()
    {
        baseSpeed = GameData.Instance.Speed;
        baseNumber = GameData.Instance.BulletNumber;
        bullets = GameObject.Find("BulletParent");
        bullet_Big = Resources.Load<GameObject>("Bullet/Bullet_Cycle_Big");
        bullet_Common = Resources.Load<GameObject>("Bullet/Bullet_Cycle_Common");


        player_Transform = GameObject.FindWithTag("playerMesh").transform;
        m_Shot = gameObject.GetComponent<BulletShotHelper>();
        m_Move = gameObject.GetComponent<EnemyActHelper>();
        m_Transform = gameObject.transform;


        m_Transform.position = new Vector3(300, 0.0f, 260);
        m_Transform.rotation = Quaternion.Euler(new Vector3(0.0f, -90f, 90f));


        StartCoroutine("StartAct");

    }


    private IEnumerator StartAct()
    {
        m_Move.StartLineMove(new Vector3(150, 0.0f, 260f), 2.0f);
        yield return new WaitForSeconds(5.0f);


        m_Move.StartTriangleMove(-150, 100, 8, 4, true);
        StartCoroutine(Skill_1(30));
        yield return new WaitForSeconds(30f);
        m_Move.StartLineMove(new Vector3(150, 0.0f, 260f), 2.0f);
        yield return new WaitForSeconds(3.0f);



        StartCoroutine(Skill_2(30));
        m_Move.StartLineMove(new Vector3(100, 50.0f, 260f), 4.0f);
        yield return new WaitForSeconds(6.0f);
        m_Move.StartAngleMove(50, -50, 120, 2, 3, 0);
        yield return new WaitForSeconds(15f);
        m_Move.StartLineMove(new Vector3(150, 0.0f, 260f), 4.0f);
        yield return new WaitForSeconds(8.0f);


        StartCoroutine(Skill_3(60));
        yield return new WaitForSeconds(2.0f);
        m_Move.StartLineMove(new Vector3(150, -50.0f, 260f), 4.0f);
        yield return new WaitForSeconds(4.0f);
        m_Move.StartSquareMove(-150,100,8,5,false);
        yield return new WaitForSeconds(50);
        m_Move.StartLineMove(new Vector3(150, 0.0f, 260f), 4.0f);
        yield return new WaitForSeconds(6.0f);
        m_Move.StartLineMove(new Vector3(300, 0.0f, 260f), 4.0f);
        yield return new WaitForSeconds(6.0f);
        gameObject.SetActive(false);
    }


    // 第一个技能
    private IEnumerator Skill_1(float totalTime)
    {
        float time = 0.0f;
        int index_1 = m_Shot.StartEuler(bullet_Big, bullets, baseNumber * 3, 60, -60, 2.0f);
        bool flag = true;
        while (true)
        {
            int index;
            index = m_Shot.StartCycleLinesWithTimeOffsetAndTrace(bullet_Common, bullets, player_Transform.position,
                38 , 10, flag, 100,baseSpeed * 2,0.5f,1.0f);
            yield return new WaitForSeconds(1.6f);
            m_Shot.StopCoroutineWithIndex(index);
            flag = !flag;
            time += 1.5f;
            if (time >= totalTime)
            {
                break;
            }
        }
        m_Shot.StopCoroutineWithIndex(index_1);
    }


    // 第二个技能
    private IEnumerator Skill_2(float totalTime)
    {
        float time = 0.0f;
        while (true)
        {
            int index;
            index = m_Shot.StartCycleBoom(bullet_Big, bullet_Common,bullets,
                baseNumber * 3, 9, -baseSpeed, baseSpeed, 0.1f,0.5f);
            yield return new WaitForSeconds(1.0f);
            m_Shot.StopCoroutineWithIndex(index);
            time += 1.0f;
            if (time >= totalTime)
            {
                break;
            }
        }
    }

    // 第三个技能
    private IEnumerator Skill_3(float totalTime)
    {
        float time = 0.0f;
        int index;
        bool flag = true;
        index = m_Shot.StartRotationLines(bullet_Big, bullets, baseNumber, 10, baseSpeed, 0.2f);
        while (true)
        {
            int index_1;

            index_1 = m_Shot.StartCycleLines(bullet_Common, bullets, baseNumber * 6, 10, flag, baseSpeed, 0.1f);
            yield return new WaitForSeconds(3.0f);
            m_Shot.StopCoroutineWithIndex(index_1);
            time += 3.0f;
            flag = !flag;
            if (time >= totalTime)
            {
                break;
            }
        }
        m_Shot.StopCoroutineWithIndex(index);
    }
}
