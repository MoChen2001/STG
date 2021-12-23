using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipOne : Ships
{
    private BulletShotHelper m_Shot;
    private EnemyActHelper m_Move;

    private Transform playerTrasform;

    private int life;


    // y -100,100
    // x 300, 150
    private void Awake()
    {
        life = 20;
        m_Move = gameObject.GetComponent<EnemyActHelper>();
        m_Shot = gameObject.GetComponent<BulletShotHelper>();

        playerTrasform = GameObject.FindGameObjectWithTag("playerMesh").transform;

    }




    // 第一种攻击方式
    /// <summary>
    /// 第一种攻击方式，向前线性移动的方式
    /// </summary>
    /// <param name="flag">为真时是线性追踪，为假时是欧拉角射击</param>
    public void LinePushLineShotAttack(bool flag = true)
    {
        
        m_Move.StartLineMove(new Vector3(150f, gameObject.transform.position.y, 260f),2.0f);
        if(flag)
        {
            StartCoroutine(ShotForLine());
        }
        else
        {
            StartCoroutine(ShotForEuler());
        }
    }


    // 间隔一段时间进行线性追踪射击
    private IEnumerator ShotForLine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1.0f);
            int index = m_Shot.StartMoreLineWithTimeOffsetAndTrace(Bullet_Common, Bullets, playerTrasform.position,
            BaseNumber * 3, 30, -BaseSpeed / 2, BaseSpeed, 1.2f);
            yield return new WaitForSeconds(3.0f);
            m_Shot.StopCoroutineWithIndex(index);
        }
    }


    // 间隔一段时间进行欧拉角射击
    private IEnumerator ShotForEuler()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            int index = m_Shot.StartEuler(Bullet_Common, Bullets,BaseNumber * 3, 30, -BaseSpeed, 1.2f);
            yield return new WaitForSeconds(3.0f);
            m_Shot.StopCoroutineWithIndex(index);
        }
    }

    // 第二种攻击方式



    /// <summary>
    /// 
    /// </summary>
    /// <param name="flag"></param>
    public void NinthLinePushLineShotAttack(int offsetx,bool flag = true)
    {

        m_Move.StartLineMove(new Vector3(offsetx, gameObject.transform.position.y, 260f), 2.0f);
        if (flag)
        {
            StartCoroutine(ShotForLine());
        }
        else
        {
            StartCoroutine(ShotForEuler());
        }
    }


    public override void MinusLife()
    {
        life--;
        if(life <= 0)
        {
            m_Shot.StopAllCoroutines();
            StopAllCoroutines();
            gameObject.SetActive(false);
            GameObject.Destroy(gameObject,3.0f);
        }

    }
}
