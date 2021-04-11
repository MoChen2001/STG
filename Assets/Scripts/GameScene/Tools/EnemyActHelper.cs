using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyActHelper : MonoBehaviour
{
    // 需要用到的通用变量
    private Transform m_Transform;
    private Vector3 initPos;


    private void Awake()
    {
        m_Transform = gameObject.transform;
        initPos = m_Transform.position;
        //StartLineMove(new Vector3(-100, 0, 0) + initPos,3);
        //StartSquareMove(50, 50, 3.0f,2.0f ,false);
        //StartAngleMove(-20,0,180,18,0.1f,0.0f);

    }


    //-------------------------------------------工具方法----------------------------------------------------

    /// <summary>
    /// 判断是否到达目标点的函数
    /// </summary>
    /// <param name="beginPos">起始点</param>
    /// <param name="endPos">目标点</param>
    /// <param name="nowPos">当前位置</param>
    /// <returns></returns>
    private bool GetPos(Vector3 beginPos,Vector3 endPos,Vector3 nowPos)
    {
        float throeyLength = Vector3.Distance(endPos, beginPos);
        float trueLength = Vector3.Distance(nowPos, beginPos);
        if(trueLength >= throeyLength)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /// <summary>
    /// 转换角度进制的函数
    /// </summary>
    /// <param name="angle">欧拉进制的角度/param>
    /// <returns></returns>
    private float ConvertAngleToPI(float angle)
    {
        return angle / 360 * 2 * Mathf.PI;
    }


    /// <summary>
    /// 根据角度和圆心以及当前的位置，获得一个带有x,y偏移量的Vector2
    /// </summary>
    /// <param name="pos">起始点</param>
    /// <param name="angle">旋转角度</param>
    /// <param name="point">旋转围绕点,圆心点</param>
    /// <returns></returns>
    private Vector2 GetRotateOffset(Vector3 pos ,float angle,Vector3 point)
    {
        Vector3 vec = pos - point;
        float radius = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        vec.x /= radius;
        vec.y /= radius;


        Vector3 vec_x = new Vector3(1.0f, 0.0f, 0.0f);
        float currentAngle = Vector3.Angle(vec, vec_x);

        float eagerAngle = ConvertAngleToPI(currentAngle + angle);
        currentAngle = ConvertAngleToPI(currentAngle);


        float currentX = Mathf.Cos(currentAngle);
        float currentY = Mathf.Sin(currentAngle);


        float eagerX = Mathf.Cos(eagerAngle);
        float eagerY = Mathf.Sin(eagerAngle);


        float offsetX = (eagerX - currentX) * radius;
        float offsetY = (eagerY - currentY) * radius;

        Vector2 result =  new Vector3(offsetX, offsetY);
        return result;
    }





    //-------------------------------------------简单的线性移动----------------------------------------------------

    /// <summary>
    /// 简单的线性移动-
    /// </summary>
    /// <param name="endPos">要到达的目标点</param>
    /// <param name="overTime">overTime 是希望移动所使用的事件</param>
    private IEnumerator SmaplerLineMove(Vector3 endPos,float overTime)
    {
        bool getPos = false;
        Vector3 beginPos = m_Transform.position;
        Vector3 pos = (endPos - m_Transform.position) / (overTime * 50);
        while(!getPos)
        {
            Vector3 temp = endPos - m_Transform.position;
            yield return new WaitForSeconds(0.02f);
            m_Transform.position += pos;
            if (GetPos(beginPos,endPos,m_Transform.position))
            {
                getPos = true;
            }
        }

    }


    /// <summary>
    /// 简单的线性移动-
    /// </summary>
    /// <param name="endPos">要到达的目标点</param>
    /// <param name="overTime">overTime 是希望移动所使用的事件</param>
    public void StartLineMove(Vector3 endPos, float overTime = 2.0f)
    {
        StartCoroutine(SmaplerLineMove(endPos, overTime));
    }





    //-------------------------------------------简单的三角移动----------------------------------------------------

    /// <summary>
    /// 以当前位置为起点向前进行三角移动
    /// </summary>
    /// <param name="offsetX">是x方向上的偏移</param>
    /// <param name="offsetY">是y方向上的偏移</param>
    /// <param name="overTime">每次移动所花的时间</param>
    /// <param name="stayTime">中间停留的时间</param>
    /// <param name="clockWise">决定顺时针还是逆时针，如果偏移点在左边，就是默认逆时针，否则是默认顺时针</param>
    /// <returns></returns>
    private IEnumerator SamplerTriangleMove(float offsetX,float offsetY,float overTime,float stayTime,bool clockWise)
    {
        float times = overTime * 50;

        Vector3 beginPos = m_Transform.position;
        Vector3 topPos = new Vector3(offsetX, offsetY, 0.0f) + m_Transform.position;
        Vector3 bottomPos = new Vector3(offsetX, -offsetY, 0.0f) + m_Transform.position;
        Vector3 firstOffset, secondOffset, lastOffset, firstPos, secondPos;
        if (clockWise)
        {
            firstPos = topPos;
            secondPos = bottomPos;
        }
        else
        {
            firstPos = bottomPos;
            secondPos = topPos;
        }
        firstOffset = (firstPos - beginPos) / times;
        secondOffset = (secondPos - firstPos) / times;
        lastOffset = (beginPos - secondPos) / times;
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            m_Transform.position += firstOffset;
            if (GetPos(beginPos, firstPos, m_Transform.position))
            {
                break;
            }
        }
        yield return new WaitForSeconds(stayTime);
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            m_Transform.position += secondOffset;
            if (GetPos(firstPos, secondPos, m_Transform.position))
            {
                break;
            }
        }
        yield return new WaitForSeconds(stayTime);
        while (true)
        {
            yield return new WaitForSeconds(0.02f);
            m_Transform.position += lastOffset;
            if (GetPos(secondPos, beginPos, m_Transform.position))
            {
                break;
            }
        }
    }

    /// <summary>
    /// 以当前位置为起点向前进行三角移动
    /// </summary>
    /// <param name="offsetX">是x方向上的偏移</param>
    /// <param name="offsetY">是y方向上的偏移</param>
    /// <param name="overTime">每次移动所花的时间</param>
    /// <param name="stayTime">中间停留的时间</param>
    /// <param name="clockWise">决定顺时针还是逆时针，如果偏移点在左边，就是默认逆时针，否则是默认顺时针</param>
    /// <returns></returns>
    public void StartTriangleMove(float offsetX, float offsetY, float overTime,
        float stayTime = 2.0f, bool clockWise = true)
    {
        StartCoroutine(SamplerTriangleMove(offsetX, offsetY, overTime, stayTime, clockWise));
    }





    //-------------------------------------------简单的正方移动----------------------------------------------------
    private IEnumerator SamplerSquareMove(Vector3 pos, float overTime, float stayTime, bool clockWise)
    {
        float times = overTime * 50;
        Vector3[] vec = new Vector3[5];
        vec[2] = pos;
        vec[0] = m_Transform.position;
        vec[4] = m_Transform.position;
        float offsetX = pos.x - vec[0].x;
        float offsetY = pos.y - vec[0].y;
        if (clockWise)
        {
            vec[1] = new Vector3(0.0f, offsetY, 0.0f) + vec[0];
            vec[3] = new Vector3(offsetX, 0.0f, 0.0f) + vec[0];
        }
        else
        {
            vec[1] = new Vector3(offsetX, 0.0f, 0.0f) + vec[0];
            vec[3] = new Vector3(0.0f, offsetY, 0.0f) + vec[0];
        }
        for(int i = 0; i < 4; i++)
        {
            while(true)
            {
                yield return new WaitForSeconds(0.02f);
                Vector3 offset = (vec[i+1] - vec[i]) / times;
                m_Transform.position += offset;
                if (GetPos(vec[i],vec[i +1],m_Transform.position))
                {
                    break;
                }
            }
            yield return new WaitForSeconds(stayTime);
        }


    }


    /// <summary>
    /// 以当前位置为起点向前进行正方移动
    /// </summary>
    /// <param name="offsetX">是x方向上的偏移</param>
    /// <param name="offsetY">是y方向上的偏移</param>
    /// <param name="overTime">每次移动所花的时间</param>
    /// <param name="stayTime">中间停留的时间</param>
    /// <param name="clockWise">决定顺时针还是逆时针，如果偏移点在左边，就是默认逆时针，否则是默认顺时针</param>
    /// <returns></returns>
    public void StartSquareMove(float offsetX,float offsetY,float overTime = 2.0f,
        float stayTime = 2.0f,bool clockWise = true)
    {
        StartCoroutine(SamplerSquareMove(new Vector3(offsetX, offsetY,m_Transform.position.z),
            overTime, stayTime, clockWise));
    }





    //-------------------------------------------简单的圆弧移动----------------------------------------------------

    /// <param name="offsetX">是圆弧运动的原点相对于现在位置的偏移</param>
    /// <param name="offsetY">是圆弧运动的原点相对于现在位置的偏移</param>
    /// <param name="angle">圆弧运动的角度</param>
    /// <param name="stayCount">中间停留的次数</param>
    /// <param name="overtime">完成每次移动使用的时间</param>
    /// <param name="stayTime">每次停留的时间</param>
    /// <returns></returns>
    private IEnumerator SamplerAngleMove(float offsetX,float offsetY,float angle,
        float stayCount,float overtime,float stayTime)
    {
        float times = overtime * 100;
        float offsetAngle = angle / (stayCount + 1);
        Vector3 pos = m_Transform.position;
        Vector3 point = pos + new Vector3(offsetX,offsetY,0.0f);
        for(int i = 0; i <= stayCount; i++)
        {
            yield return new WaitForSeconds(stayTime);
            pos = m_Transform.position;
            Vector2 offset = GetRotateOffset(pos, offsetAngle, point);
            Vector3 target = pos + new Vector3(offset.x, offset.y, 0.0f);
            Vector3 perOffset = new Vector3(offset.x, offset.y, 0.0f) / times;
            while (true)
            {

                yield return new WaitForSeconds(0.01f);
                m_Transform.position += perOffset;
                if(GetPos(pos,target,m_Transform.position))
                {
                    break;
                }
            }
        }

    }


    /// <param name="offsetX">是圆弧运动的原点相对于现在位置的偏移</param>
    /// <param name="offsetY">是圆弧运动的原点相对于现在位置的偏移</param>
    /// <param name="angle">圆弧运动的角度</param>
    /// <param name="stayCount">中间停留的次数</param>
    /// <param name="overtime">完成每次移动使用的时间</param>
    /// <param name="stayTime">每次停留的时间</param>
    /// <returns></returns>
    public void StartAngleMove(float offsetX, float offsetY, float angle = 90,
        float stayCount = 2, float overtime = 2.0f, float stayTime = 2.0f)
    {
        StartCoroutine(SamplerAngleMove(offsetX,offsetY,angle,stayCount,overtime,stayTime));
    }

}
