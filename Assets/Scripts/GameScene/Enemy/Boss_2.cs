using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_2 : MonoBehaviour
{
    // 通用变量
    private BulletShotHelper m_Shot;
    private EnemyActHelper m_Move;
    private Transform m_Transform;
    private Transform player_Transform;



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
        m_Transform.rotation = Quaternion.Euler(new Vector3(0.0f,-90f,90f));


        StartCoroutine("StartAct");

    }



    // 执行的所有的技能方法
    private IEnumerator StartAct()
    {
        m_Move.StartLineMove(new Vector3(150, 0.0f, 260f), 2.0f);
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(StartTriangleAndCulerShot_1(20));
        yield return new WaitForSeconds(20.0f);

        m_Move.StartLineMove(new Vector3(200, 0.0f, 260f), 5.0f);
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(StartEulerTraceShot_2(20));
        yield return new WaitForSeconds(20.0f);
        yield return new WaitForSeconds(3.0f);
        m_Move.StartTriangleMove(-200, 100, 6, 6, true);
        StartCoroutine(StartThred_3(30));
        yield return new WaitForSeconds(30.0f);
        yield return new WaitForSeconds(3.0f);

        m_Move.StartLineMove(new Vector3(150, -100f, 260f), 2.0f);
        yield return new WaitForSeconds(2.0f);
        StartCoroutine(StartSquareMoveCycleShot(60));
        m_Move.StartSquareMove(-200, 100, 9, 6, true);
        yield return new WaitForSeconds(60.0f);
        m_Move.StartLineMove(new Vector3(200, 0.0f, 260f), 2.0f);
        yield return new WaitForSeconds(2.0f);
        m_Move.StartLineMove(new Vector3(300, 0.0f, 260f), 2.0f);
        yield return new WaitForSeconds(2.0f);
        UIStateController.Instance.PassState();

    }




    // 第一个技能方法
    private IEnumerator StartTriangleAndCulerShot_1(float totalTime)
    {
        float time = 0.0f;
        int index_1 = m_Shot.StartEuler(bullet_Big, bullets, baseNumber * 2, 60, -60, 2.0f);
        while (true)
        {
            int index;
            index = m_Shot.StartMoreLineWithTimeOffsetAndTrace(bullet_Common, bullets, player_Transform.position,
                baseNumber * 3, 20,-baseSpeed, baseSpeed, 0.2f);
            yield return new WaitForSeconds(1.0f);
            m_Shot.StopCoroutineWithIndex(index);
            time += 1.0f;
            if (time >= totalTime)
            {
                break;
            }
        }
        m_Shot.StopCoroutineWithIndex(index_1);
    }


    // 第二个技能方法
    private IEnumerator StartEulerTraceShot_2(float Totaltime)
    {
        float time = 0.0f;
        while (true)
        {
            int index;
            index = m_Shot.StartCycleWithTimeOffsetAndTrace(bullet_Common, bullets, player_Transform.position,
                baseNumber * 6, baseSpeed, baseSpeed * 2, 0.2f, 1.0f);
            yield return new WaitForSeconds(2.1f);
            m_Shot.StopCoroutineWithIndex(index);
            time += 2.1f;
            if (time >= Totaltime)
            {
                break;
            }
        }
    }


    // 第三个技能方法
    private IEnumerator StartThred_3(float totalTime)
    {
        float time = 0.0f;
        int index;
        if(baseNumber * 4 >= 24)
        {
            baseNumber = 24;
        }
        while (true)
        {
           index = m_Shot.StartCycleBoom(bullet_Big,bullet_Common,bullets,baseNumber,baseNumber,
                baseSpeed / 2,baseSpeed,0.5f,0.3f);
            yield return new WaitForSeconds(1.5f);
            time += 1.5f;
            m_Shot.StopCoroutineWithIndex(index);
            if(time >= totalTime)
            {
                break;
            }
        }
    }


    // 第四个技能方法
    private IEnumerator StartSquareMoveCycleShot(float totalTime)
    {
        float time = 0.0f;
        int index;
        bool dirty = false;
        if (baseNumber * 3 >= 36)
        {
            baseNumber = 36;
        }
        else
        {
            baseNumber *= 3;
        }
        while (true)
        {
            index = m_Shot.StartMoreRotationLines(bullet_Common, bullets, baseNumber, 90, baseSpeed, 0.1f);
            yield return new WaitForSeconds(2.0f);
            m_Shot.StopCoroutineWithIndex(index);
            if(!dirty && baseNumber <= 18)
            {
                baseNumber *= 2;
                dirty = true;
            }
            time += 2.0f;
            if(time >= totalTime)
            {
                break;
            }
        }
    }
}
