using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShotHelper : MonoBehaviour
{

    // 发射子弹的辅助类 ，注意每一个的速度都可以控制方向
    //-------------------------------------------------初始化数据--------------------------------------------------------

    public static BulletShotHelper Instance;


    private static int currentIndex = 0;

    private Dictionary<int, Coroutine> coroutinesDic;


    private Vector3 firePos;



    private void Awake()
    {
        currentIndex = 0;

        coroutinesDic = new Dictionary<int, Coroutine>();


        firePos = gameObject.transform.position;
    }


    private Vector3 NormalizeVector3InZ(Vector3 vec)
    {
        float radius = Mathf.Sqrt(vec.x * vec.x + vec.y * vec.y);
        vec.x /= radius;
        vec.y /= radius;
        vec.z = 0.0f;
        return vec;
    }



    private void Update()
    {
        firePos = gameObject.transform.position;
        if (UIStateController.Instance.gameOver)
        {
            StopAllCoroutines();
        }
    }







    //-------------------------------------------------工具方法--------------------------------------------------------

    private void StopMove(GameObject obj)
    {
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        obj.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }


    // 对字典进行数据的添加并返回索引
    private int GetIndex(Coroutine coroutine)
    {
        int index = currentIndex;
        coroutinesDic.Add(index, coroutine);
        return currentIndex;
    }

    // 根据索引停止协程
    public void StopCoroutineWithIndex(int index)
    {
        StopCoroutine(coroutinesDic[index]);
        if(coroutinesDic.ContainsKey(index))
        {
            coroutinesDic.Remove(index);
        }
    }


    // 对弹幕进行初始化
    // target 是发射的目标点
    private void InitBullet(GameObject bullet, GameObject parent, Vector3 target, float speed,float waitTime = 0.0f)
    {
        StartCoroutine(InitBulletWithTimeOffset(bullet, parent, target, speed, waitTime));
    }


    // 创造单个弹幕
    private GameObject CreateOneBullet(GameObject bullet, Vector3 firePosition)
    {
        GameObject temp = GameObject.Instantiate(bullet, firePosition, Quaternion.identity);
        return temp;
    }

    // 创造一个弹幕圈
    private GameObject[] CreateCycleBullet(GameObject bullet,GameObject parent, Vector3 firePosition, 
        int numbers = 12,float speed = 40f)
    {
        float rotateAngle = 0;
        GameObject[] temps = new GameObject[numbers];
        for (int i = 0; i < numbers; i++)
        {

            rotateAngle = (rotateAngle / 360) * Mathf.PI * 2 ;
            float offsetX = 1.0f * Mathf.Cos(rotateAngle);
            float offsetY = 1.0f * Mathf.Sin(rotateAngle);

            Vector3 pos =  new Vector3(offsetX, offsetY, 0.0f);
            temps[i] = CreateOneBullet(bullet, firePosition);
            if(temps[i].activeSelf)
            {
                InitBullet(temps[i], parent, pos, speed);
            }

            rotateAngle = (rotateAngle / (Mathf.PI * 2 )) * 360;
            rotateAngle += 360 / numbers;

        }
        return temps;
    }

    // 圆形线性弹幕的使用
    private GameObject[] CreateCycleBulletRotate(GameObject bullet, GameObject parent, Vector3 firePosition,
    int numbers = 12, float offsetAngle = 10,  float speed = 40.0f)
    {
        float rotateAngle = offsetAngle;
        GameObject[] temps = new GameObject[numbers];
        for (int i = 0; i < numbers; i++)
        {

            rotateAngle = (rotateAngle / 360) * Mathf.PI * 2;
            float offsetX = 1.0f * Mathf.Cos(rotateAngle);
            float offsetY = 1.0f * Mathf.Sin(rotateAngle);

            Vector3 pos = firePosition + new Vector3(offsetX, offsetY, 0.0f);
            temps[i] = CreateOneBullet(bullet, firePosition);
            pos -= firePos;
            if (temps[i].activeSelf)
            {
                InitBullet(temps[i], parent, pos, speed);
            }



            rotateAngle = (rotateAngle / (Mathf.PI * 2)) * 360;
            rotateAngle += 360 / numbers;

        }
        return temps;
    }


    // 创造一个弹幕圈,同时目标围着目标摆动
    private GameObject[] CreateCycleBulletLinesRotate(GameObject bullet, GameObject parent, Vector3 firePosition,
        int numbers = 12, float offsetAngle = 10,float speed = 40.0f)
    {
        float rotateAngle = Mathf.Sin(Time.time) * offsetAngle;
        GameObject[] temps = new GameObject[numbers];
        for (int i = 0; i < numbers; i++)
        {

            rotateAngle = (rotateAngle / 360) * Mathf.PI * 2;
            float offsetX = 1.0f * Mathf.Cos(rotateAngle);
            float offsetY = 1.0f * Mathf.Sin(rotateAngle);

            Vector3 pos = new Vector3(offsetX, offsetY, 0.0f);
            temps[i] = CreateOneBullet(bullet, firePosition);
            if (temps[i].activeSelf)
                InitBullet(temps[i], parent, pos, speed);


            rotateAngle = (rotateAngle / (Mathf.PI * 2)) * 360;
            rotateAngle += 360 / numbers + Mathf.Sin(Time.time * 2);

        }
        return temps;
    }

    //-------------------------------------------------延迟弹幕生成工具方法--------------------------------------------------------



    // 创造一个弹幕圈,可以跟有延迟
    private GameObject[] CreateCycleBulletWithTimeOffset(GameObject bullet, GameObject parent, Vector3 firePosition,
    int numbers = 12, float speed = 10.0f, float speed_2 = 1.0f, float waitTime = 0.0f)
    {
        float rotateAngle = 0;
        GameObject[] temps = new GameObject[numbers];
        for (int i = 0; i < numbers; i++)
        {

            rotateAngle = (rotateAngle / 360) * Mathf.PI * 2;
            float offsetX = 1.0f * Mathf.Cos(rotateAngle);
            float offsetY = 1.0f * Mathf.Sin(rotateAngle);

            Vector3 pos = new Vector3(offsetX, offsetY, 0.0f);
            temps[i] = CreateOneBullet(bullet, pos + firePosition);
            if (temps[i].activeSelf)
            {
                InitBullet(temps[i], parent, pos, speed, 0.0f);
                InitBullet(temps[i], parent, pos, speed_2, waitTime);
            }

            rotateAngle = (rotateAngle / (Mathf.PI * 2)) * 360;
            rotateAngle += 360 / numbers;

        }
        return temps;
    }



    // 创造一个弹幕圈,可以跟有延迟
    private GameObject[] CreateCycleBulletWithTimeOffsetAndTrace(GameObject bullet, GameObject parent,Vector3 pos_1 ,
        Vector3 firePosition,int numbers = 12, float speed = 10.0f, float speed_2 = 1.0f, float waitTime = 0.0f)
    {
        float rotateAngle = 0;
        GameObject[] temps = new GameObject[numbers];
        for (int i = 0; i < numbers; i++)
        {

            rotateAngle = (rotateAngle / 360) * Mathf.PI * 2;
            float offsetX = 1.0f * Mathf.Cos(rotateAngle);
            float offsetY = 1.0f * Mathf.Sin(rotateAngle);

            Vector3 pos = new Vector3(offsetX, offsetY, 0.0f);
            temps[i] = CreateOneBullet(bullet, pos + firePosition);
            if (temps[i].activeSelf)
            {
                InitBullet(temps[i], parent, pos, speed, 0.0f);
                StopMove(temps[i]);
                NormalizeVector3InZ(pos_1 - temps[i].transform.position);
                InitBullet(temps[i], parent, pos, speed_2, waitTime);
            }

            rotateAngle = (rotateAngle / (Mathf.PI * 2)) * 360;
            rotateAngle += 360 / numbers;

        }
        return temps;
    }

    private IEnumerator InitBulletWithTimeOffset(GameObject bullet, GameObject parent, Vector3 target,
    float speed, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        bullet.GetComponent<Rigidbody>().AddForce(target * speed, ForceMode.Impulse);
        bullet.transform.SetParent(parent.transform);
    }


    //-------------------------------------------------延迟多行弹幕生成工具方法--------------------------------------------------------



    // 第四个参数 Range 是最上面到最下面的间隔
    private IEnumerator MoreLineShotWithTimeOffsetHelp(GameObject bullet, GameObject parent,
    int number, float range, float speed,float speed_2,
    float waitSeconds,float offsetTime)
    {
        currentIndex++;
        float distance = range / (number - 1);
        Vector3 vec = new Vector3(0.0f, range / 2, 0.0f); ;
        Vector3 offset = new Vector3(0.0f, distance, 0.0f);
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            Vector3 firePosition = firePos;
            firePosition += vec;
            for (int i = 0; i < number; i++)
            {
                GameObject temp = CreateOneBullet(bullet, firePosition);
                Vector3 target = new Vector3(1, 0.0f, 0.0f);
                if (temp.activeSelf)
                {
                    InitBullet(temp, parent, target, speed);
                    InitBullet(temp, parent, target, speed_2, offsetTime);
                }
                firePosition -= offset;
            }
        }
    }

    /// <summary>
    /// 延迟多行弹幕生成
    /// </summary>
    /// <param name="bullet">子弹预制体</param>
    /// <param name="parent">子弹父类</param>
    /// <param name="number">数量</param>
    /// <param name="range"></param>
    /// <param name="speed"></param>
    /// <param name="waitSeconds"></param>
    /// <param name="offsetTime"></param>
    /// <returns></returns>
    public int StartMoreLineWithTimeOffset(GameObject bullet, GameObject parent,
    int number = 1, float range = 20f,float speed = 10f, float speed_2 = 20, 
    float waitSeconds = 0.1f, float offsetTime = 0.0f)
    {
        Coroutine temp = StartCoroutine(MoreLineShotWithTimeOffsetHelp(bullet, parent,
             number, range,speed, speed_2, waitSeconds, offsetTime));
        int index = GetIndex(temp);
        return index;

    }





    //-------------------------------------------------延迟多行追踪弹幕生成工具方法--------------------------------------------------------



    // 第四个参数 Range 是最上面到最下面的间隔
    private IEnumerator MoreLineShotWithTimeOffsetHelp(GameObject bullet, GameObject parent,Vector3 pos,
    int number, float range, float speed, float speed_2,
     float offsetTime)
    {
        currentIndex++;
        float distance = range / (number - 1);
        Vector3 vec = new Vector3(0.0f, range / 2, 0.0f); ;
        Vector3 offset = new Vector3(0.0f, distance, 0.0f);
        while (true)
        {
            GameObject[] temp = new GameObject[number];

            Vector3 firePosition = firePos;
            firePosition += vec;
            for (int i = 0; i < number; i++)
            {
                temp[i] = CreateOneBullet(bullet, firePosition);
                Vector3 target = new Vector3(1, 0.0f, 0.0f);
                if (temp[i].activeSelf)
                {
                    InitBullet(temp[i], parent, target, speed);
                }
                firePosition -= offset;
            }
            yield return new WaitForSeconds(offsetTime);
            for (int i = 0; i < number; i++)
            {
                if (temp[i].activeSelf)
                {
                    StopMove(temp[i]);
                    Vector3 pos_1 = NormalizeVector3InZ(pos - temp[i].transform.position);
                    temp[i].GetComponent<Rigidbody>().AddForce(pos_1 * speed_2, ForceMode.Impulse);
                }
            }
        }
    }

    /// <summary>
    /// 延迟多行追踪弹幕生成
    /// </summary>
    /// <param name="bullet">子弹预制体</param>
    /// <param name="parent">子弹父类</param>
    /// <param name="number">数量</param>
    /// <param name="range"></param>
    /// <param name="speed"></param>
    /// <param name="waitSeconds"></param>
    /// <param name="offsetTime"></param>
    /// <returns></returns>
    public int StartMoreLineWithTimeOffsetAndTrace(GameObject bullet, GameObject parent, Vector3 pos,
    int number = 1, float range = 20f, float speed = 10f, float speed_2 = 20, float offsetTime = 0.0f)
    {
        Coroutine temp = StartCoroutine(MoreLineShotWithTimeOffsetHelp(bullet, parent, pos,
             number, range, speed, speed_2,  offsetTime));
        int index = GetIndex(temp);
        return index;

    }




    //-------------------------------------------------延迟散射弹幕生成工具方法--------------------------------------------------------

    private IEnumerator EulerShotWithOffsetHelp(GameObject bullet, GameObject parent,
    float number, float eulerAngle, float speed, float speed_2,float waitSeconds,float offsetTime)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            Vector3 firePosition = firePos;
            float tempAngle = eulerAngle / 2;
            for (int i = 0; i < number; i++)
            {
                GameObject temp = CreateOneBullet(bullet, firePosition);


                tempAngle = (tempAngle / 360) * (Mathf.PI * 2);

                float offsetX = 1.0f * Mathf.Cos(tempAngle);
                float offsetY = 1.0f * Mathf.Sin(tempAngle);
                Vector3 target = firePosition + new Vector3(offsetX, offsetY, 0.0f);
                target -= firePosition;
                if (temp.activeSelf)
                {
                    InitBullet(temp, parent, target, speed);
                    InitBullet(temp, parent, target, speed_2, offsetTime);
                }
                tempAngle = (tempAngle / (Mathf.PI * 2)) * 360;
                tempAngle -= eulerAngle / (number - 1);
            }

        }
    }


    public int StartEulerWithTimeOffset(GameObject bullet, GameObject parent, float number = 2,
        float eulerAngle = 60, float speed = 10f,float speed_2 = 1, float waitSeconds = 0.5f, float offsetTime = 0.0f)
    {

        Coroutine temp = StartCoroutine(EulerShotWithOffsetHelp(bullet, parent, number, eulerAngle, 
            speed, speed_2, waitSeconds, offsetTime));
        int index = GetIndex(temp);
        return index;
    }


    //-------------------------------------------------延迟散射追踪弹幕生成工具方法--------------------------------------------------------

    private IEnumerator EulerShotWithOffsetAndTraceHelp(GameObject bullet, GameObject parent,Vector3 pos,
    float number, float eulerAngle, float speed, float speed_2, float waitSeconds, float offsetTime)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            Vector3 firePosition = firePos;
            GameObject[] temp = new GameObject[(int)number];
            float tempAngle = eulerAngle / 2;
            for (int i = 0; i < number; i++)
            {
                temp[i] = CreateOneBullet(bullet, firePosition);


                tempAngle = (tempAngle / 360) * (Mathf.PI * 2);

                float offsetX = 1.0f * Mathf.Cos(tempAngle);
                float offsetY = 1.0f * Mathf.Sin(tempAngle);
                Vector3 target = firePosition + new Vector3(offsetX, offsetY, 0.0f);
                target -= firePosition;
                if (temp[i].activeSelf)
                    InitBullet(temp[i], parent, target, speed);
                tempAngle = (tempAngle / (Mathf.PI * 2)) * 360;
                tempAngle -= eulerAngle / (number - 1);
            }
            yield return new WaitForSeconds(offsetTime);
            for (int i = 0; i < number; i++)
            {
                if (temp[i].activeSelf)
                {
                    StopMove(temp[i]);
                    InitBullet(temp[i], parent, NormalizeVector3InZ(pos - temp[i].transform.position), speed_2);
                }
            }

        }
    }


    public int StartEulerWithTimeOffsetAndTrace(GameObject bullet, GameObject parent, Vector3 pos, float number = 2,
        float eulerAngle = 60, float speed = 10f, float speed_2 = 1, float waitSeconds = 0.5f, float offsetTime = 0.0f)
    {

        Coroutine temp = StartCoroutine(EulerShotWithOffsetAndTraceHelp(bullet, parent, pos, number, eulerAngle,
            speed, speed_2, waitSeconds, offsetTime));
        int index = GetIndex(temp);
        return index;
    }







    //-------------------------------------------------圆形线性延迟旋转追踪弹幕生成方法--------------------------------------------------------


    private IEnumerator CycleLinesShotWithTimeOffsetHelp(GameObject bullet, GameObject parent, Vector3 pos,int lineNumber = 6,
   float angleDistance = 10, bool clock = true, float speed = 2.0f,
   float speed_2 = 2.0f, float watiSceond = 0.2f, float timeOffset = 0.5f, float totalTime = 2.0f)
    {
        currentIndex++;
        while (true)
        {
            GameObject[] temps = new GameObject[lineNumber];
            int i = 0;
            while (i < lineNumber)
            {
                yield return new WaitForSeconds(0.01f);
                //temps[i] = CreateCycleBulletRotate(bullet, parent, firePos, 1, angleDistance, speed)[0];
                temps[i] = CreateCycleBulletRotate(bullet, parent, firePos, 1, angleDistance, speed)[0];
                i++;
                if (clock)
                {
                    angleDistance += 10;
                }
                else
                {
                    angleDistance -= 10;
                }
            }
            yield return new WaitForSeconds(timeOffset);
            for (int j = 0; j < temps.Length; j++)
            {
                if (temps[j].activeSelf)
                {
                    StopMove(temps[j]);
                    InitBullet(temps[j], parent, NormalizeVector3InZ(pos - temps[j].transform.position), speed_2, 0.1f);
                }
            }
            yield return new WaitForSeconds(totalTime);

        }
    }






    /// <summary>
    /// 生成一波一波的环形弹幕
    /// </summary>
    /// <param name="bullet">子弹预制体</param>
    /// <param name="parent">子弹父类</param>
    /// <param name="lineNumber">一圈的子弹个数</param>
    /// <param name="angleDistance">角度之间的距离</param>
    /// <param name="clock">顺时针还是逆时针</param>
    /// <param name="flag">向左还是向右</param>
    /// <param name="speed">第一次发射的速度</param>
    /// <param name="speed_2">第二次发射的速度</param>
    /// <param name="timeOffset">第一次和第二次加速时的时间延迟</param>
    /// <param name="totalTime">每一波等待的时间</param>
    /// <returns></returns>
    public int StartCycleLinesWithTimeOffsetAndTrace(GameObject bullet, GameObject parent, Vector3 pos, int lineNumber = 6,
   float angleDistance = 10, bool clock = true,  float speed = 2.0f,
   float speed_2 = 2.0f, float timeOffset = 0.5f, float totalTime = 2.0f)
    {
        Coroutine temp = StartCoroutine(CycleLinesShotWithTimeOffsetHelp(bullet, parent, pos, lineNumber, angleDistance,
            clock,  speed, speed_2, timeOffset, totalTime));

        int index = GetIndex(temp);
        return index;
    }




    //-------------------------------------------------圆形延迟弹幕生成方法--------------------------------------------------------

    // 直接调用上面的工具方法就可以，实际上跟散射弹幕的原理差不多
    private IEnumerator CycleShotWithTimeOffsetHelp(GameObject bullet, GameObject parent,
        int number, float speed,float  speed_2,float waitSeconds,float offsetTime)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            CreateCycleBulletWithTimeOffset(bullet, parent, firePos, number, speed,speed_2,offsetTime);
        }
    }


    public int StartCycleWithTimeOffset(GameObject bullet, GameObject parent,int number = 6,
     float speed = 10f, float speed_2 = 1.0f,float waitSeconds = 0.5f,float offsetTime = 0.0f)
    {

        Coroutine temp = StartCoroutine(CycleShotWithTimeOffsetHelp(bullet, parent, number, 
            speed, speed_2, waitSeconds, offsetTime));
        int index = GetIndex(temp);
        return index;
    }



    //-------------------------------------------------圆形延迟追踪弹幕生成方法--------------------------------------------------------

    // 直接调用上面的工具方法就可以，实际上跟散射弹幕的原理差不多
    private IEnumerator CycleShotWithTimeOffsetWithTraceHelp(GameObject bullet, GameObject parent,Vector3 pos,
        int numbers, float speed, float speed_2, float waitSeconds, float offsetTime)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            float rotateAngle = 0;
            GameObject[] temps = new GameObject[numbers];
            for (int i = 0; i < numbers; i++)
            {

                rotateAngle = (rotateAngle / 360) * Mathf.PI * 2;
                float offsetX = 1.0f * Mathf.Cos(rotateAngle);
                float offsetY = 1.0f * Mathf.Sin(rotateAngle);

                Vector3 pos_1 = new Vector3(offsetX, offsetY, 0.0f);
                temps[i] = CreateOneBullet(bullet, firePos);
                if(temps[i].activeSelf)
                {
                    temps[i].GetComponent<Rigidbody>().AddForce(pos_1 * speed, ForceMode.Impulse);
                    temps[i].transform.SetParent(parent.transform);
                }
                rotateAngle = (rotateAngle / (Mathf.PI * 2)) * 360;
                rotateAngle += 360 / numbers;
            }
            yield return new WaitForSeconds(offsetTime);
            for(int i = 0; i < numbers; i++)
            {
                if(temps[i].activeSelf)
                {
                    StopMove(temps[i]);
                    Vector3 pos_1 = NormalizeVector3InZ(pos - temps[i].transform.position);
                    temps[i].GetComponent<Rigidbody>().AddForce(pos_1 * speed, ForceMode.Impulse);
                }
            }
        }
    }


    /// <summary>
    /// 生成追踪的环形弹幕
    /// </summary>
    /// <param name="bullet"></param>
    /// <param name="parent"></param>
    /// <param name="pos">追踪位置</param>
    /// <param name="number">弹幕数量</param>
    /// <param name="speed">第一次的弹幕速度</param>
    /// <param name="speed_2">第二次的弹幕速度</param>
    /// <param name="waitSeconds">每一波等待的时间</param>
    /// <param name="offsetTime">第二次加速时的等待时间</param>
    /// <returns></returns>
    public int StartCycleWithTimeOffsetAndTrace(GameObject bullet, GameObject parent, Vector3 pos, int number = 6,
     float speed = 10f, float speed_2 = 1.0f, float waitSeconds = 0.5f, float offsetTime = 0.0f)
    {

        Coroutine temp = StartCoroutine(CycleShotWithTimeOffsetWithTraceHelp(bullet, parent, pos, number,
            speed, speed_2, waitSeconds, offsetTime));
        int index = GetIndex(temp);
        return index;
    }










    //-------------------------------------------------多行弹幕生成方法--------------------------------------------------------

    // 第四个参数 Range 是最上面到最下面的间隔
    private IEnumerator MoreLineShotHelp(GameObject bullet, GameObject parent, 
    int number, float range ,float speed, float waitSeconds)
    {
        currentIndex++;
        float distance = range / (number -1);
        Vector3 vec = new Vector3(0.0f, range / 2, 0.0f);;
        Vector3 offset = new Vector3(0.0f, distance, 0.0f);
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            Vector3 firePosition = firePos;
            firePosition += vec;
            for (int i = 0; i < number; i++)
            {
                GameObject temp = CreateOneBullet(bullet, firePosition);
                Vector3 target = new Vector3(1,0.0f,0.0f);
                if (temp.activeSelf)
                    InitBullet(temp, parent, target, speed);
                firePosition -= offset;
            }
        }
    }


    public int StartMoreLine(GameObject bullet, GameObject parent, 
    int number = 1, float range = 20f, float speed = 10f, float waitSeconds = 0.1f)
    {
        Coroutine temp = StartCoroutine(MoreLineShotHelp(bullet, parent,
             number, range, speed, waitSeconds));

        int index = GetIndex(temp);
        return index;

    }





    //-------------------------------------------------散射弹幕生成方法--------------------------------------------------------


    // 
    private IEnumerator EulerShotHelp(GameObject bullet, GameObject parent, 
    float number, float eulerAngle,float speed, float waitSeconds)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            Vector3 firePosition = firePos;
            float tempAngle = eulerAngle / 2;
            for (int i = 0; i < number; i++)
            {
                GameObject temp = CreateOneBullet(bullet, firePosition);


                tempAngle = (tempAngle / 360) * (Mathf.PI * 2);

                float offsetX = 1.0f * Mathf.Cos(tempAngle);
                float offsetY = 1.0f * Mathf.Sin(tempAngle);
                Vector3 target = firePosition + new Vector3(offsetX, offsetY, 0.0f);
                target -= firePosition;
                if (temp.activeSelf)
                    InitBullet(temp, parent, target, speed);

                tempAngle = (tempAngle / (Mathf.PI * 2)) * 360;
                tempAngle -= eulerAngle / (number - 1);
            }

        }
    }


    public int StartEuler(GameObject bullet, GameObject parent,  float number = 2 ,
        float eulerAngle = 60, float speed = 10f, float waitSeconds = 0.5f)
    {

        Coroutine temp = StartCoroutine(EulerShotHelp(bullet, parent, number, eulerAngle, speed, waitSeconds));
        int index = GetIndex(temp);
        return index;
    }




    //-------------------------------------------------圆形弹幕生成方法--------------------------------------------------------

    // 直接调用上面的工具方法就可以，实际上跟散射弹幕的原理差不多
    private IEnumerator CycleShotHelp(GameObject bullet,GameObject parent,
        int number,float speed ,float waitSeconds)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            CreateCycleBullet(bullet, parent, firePos, number,  speed);
        }
    }


    public int StartCycle(GameObject bullet, GameObject parent, int number = 6,
     float speed = 10f, float waitSeconds = 0.5f)
    {

        Coroutine temp = StartCoroutine(CycleShotHelp(bullet, parent, number, speed, waitSeconds));
        int index = GetIndex(temp);
        return index;
    }




    //-------------------------------------------------圆形爆炸弹幕生成方法--------------------------------------------------------
   
    private IEnumerator CycleBoomShotHelp(GameObject bullet, GameObject bullet_2, GameObject parent,
    int number_1, int number_2, float speed,float speed_2, 
    float waitSeconds,float waitSceonds_2)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(waitSeconds);
            GameObject[] temps = CreateCycleBullet(bullet, parent, firePos, number_1,  speed);
            yield return new WaitForSeconds(waitSceonds_2);
            for(int i = 0; i < temps.Length; i++)
            {
                if (temps[i].activeSelf)
                {
                    CreateCycleBullet(bullet_2, parent, temps[i].transform.position, number_2, speed_2);
                    GameObject.Destroy(temps[i]);
                }
            }
        }
    }


    public int StartCycleBoom(GameObject bullet,GameObject bullet_2, GameObject parent, 
    int number_1 = 6, int number_2 = 6,  float speed = 10.0f, float speed_2 = 5.0f,
    float waitSeconds = 0.5f, float waitSceonds_2 = 0.5f)
    {

        Coroutine temp = StartCoroutine(CycleBoomShotHelp(bullet, bullet_2, parent,  number_1
            , number_2,  speed, speed_2, waitSeconds, waitSceonds_2));

        int index = GetIndex(temp);
        return index;
    }





    //-------------------------------------------------圆形线性延迟旋转弹幕生成方法--------------------------------------------------------


    private IEnumerator CycleLinesShotWithTimeOffsetHelp(GameObject bullet, GameObject parent, int lineNumber = 6,
   float angleDistance = 10, bool clock = true, float flag = 1,float speed = 2.0f, 
   float speed_2 = 2.0f,float watiSceond = 0.2f,float timeOffset = 0.5f,float totalTime = 2.0f)
    {
        currentIndex++;
        while(true)
        {
            GameObject[] temps = new GameObject[lineNumber];
            int i = 0;
            while (i < lineNumber)
            {
                yield return new WaitForSeconds(0.01f);
                //temps[i] = CreateCycleBulletRotate(bullet, parent, firePos, 1, angleDistance, speed)[0];
                temps[i] = CreateCycleBulletRotate(bullet, parent, firePos, 1, angleDistance, speed)[0];
                i++;
                if (clock)
                {
                    angleDistance += 10;
                }
                else
                {
                    angleDistance -= 10;
                }
            }
            yield return new WaitForSeconds(timeOffset);
            for (int j = 0; j < temps.Length; j++)
            {
                if (temps[i].activeSelf)
                    InitBullet(temps[j], parent, flag * temps[j].transform.right, speed_2,0.1f);
            }
            yield return new WaitForSeconds(totalTime);

        }
    }


    /// <summary>
    /// 生成一波一波的环形弹幕
    /// </summary>
    /// <param name="bullet">子弹预制体</param>
    /// <param name="parent">子弹父类</param>
    /// <param name="lineNumber">一圈的子弹个数</param>
    /// <param name="angleDistance">角度之间的距离</param>
    /// <param name="clock">顺时针还是逆时针</param>
    /// <param name="flag">向左还是向右</param>
    /// <param name="speed">第一次发射的速度</param>
    /// <param name="speed_2">第二次发射的速度</param>
    /// <param name="timeOffset">第一次和第二次加速时的时间延迟</param>
    /// <param name="totalTime">每一波等待的时间</param>
    /// <returns></returns>
    public int StartCycleLinesWithTimeOffset(GameObject bullet, GameObject parent, int lineNumber = 6,
   float angleDistance = 10, bool clock = true, float flag = 1, float speed = 2.0f,
   float speed_2 = 2.0f,  float timeOffset = 0.5f, float totalTime = 2.0f)
    {
        Coroutine temp = StartCoroutine(CycleLinesShotWithTimeOffsetHelp(bullet, parent, lineNumber,angleDistance,
            clock, flag,speed, speed_2, timeOffset , totalTime));

        int index = GetIndex(temp);
        return index;
    }




    //-------------------------------------------------多行螺旋形弹幕生成方法--------------------------------------------------------



    // radius是弹幕的更新距离，angleDistance是每次旋转的角度
    private IEnumerator RotationLinesShotHelp(GameObject bullet, GameObject parent, int lineNumber,
         float angleDistance, float speed, float watiSceond)
    {
        currentIndex++;
        float rotateAngle = 0.0f;
        while (true)
        {
            yield return new WaitForSeconds(watiSceond);
            rotateAngle = (rotateAngle / 360) * 2 * Mathf.PI;
            float offsetX =  1 * Mathf.Cos(rotateAngle);
            float offsetY =  1 * Mathf.Sin(rotateAngle);
            Vector3 offset = new Vector3(offsetX, offsetY, 0.0f);
            Vector3 targetPos = 2 *  offset;

            GameObject[] temps = CreateCycleBullet(bullet, parent, firePos, lineNumber,  speed);

            for(int i = 0; i < temps.Length; i++)
            {
                if (temps[i].activeSelf)
                    InitBullet(temps[i], parent, targetPos, speed);
            }
            rotateAngle = (rotateAngle / (2 * Mathf.PI)) * 360;
            rotateAngle += angleDistance;
        }
    }


    public int StartRotationLines(GameObject bullet, GameObject parent, int lineNumber = 6,
        float angleDistance = 10, float speed = 10f, float watiSceond = 0.2f)
    {
        Coroutine temp = StartCoroutine(RotationLinesShotHelp(bullet, parent, lineNumber,
             angleDistance, speed,  watiSceond));

        int index = GetIndex(temp);
        return index;
    }








    //-------------------------------------------------多行圆形连续弹幕生成方法--------------------------------------------------------


    private IEnumerator ContinusRotationLinesShotHelp(GameObject bullet, GameObject parent,  int lineNumber,
        float waveAngles,float speed, float watiSceond)
    {

        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(watiSceond);
            CreateCycleBulletLinesRotate(bullet, parent, firePos, lineNumber, waveAngles, speed);
        }
    }


    public int StartMoreRotationLines(GameObject bullet, GameObject parent, int lineNumber = 6, float waveAngles = 10,
         float speed = 10f, float watiSceond = 0.2f)
    {
        Coroutine temp = StartCoroutine(ContinusRotationLinesShotHelp(bullet ,parent, lineNumber,
            waveAngles, speed, watiSceond));

        int index = GetIndex(temp);
        return index;
    }






    //-------------------------------------------------圆线性连续弹幕生成方法--------------------------------------------------------



    private IEnumerator CycleLinesShotHelp(GameObject bullet,GameObject parent,int lineNumber = 6,
       float angleDistance = 10, bool clock = true,float speed = 2.0f, float watiSceond = 0.2f)
    {
        currentIndex++;
        while (true)
        {
            yield return new WaitForSeconds(watiSceond);
            CreateCycleBulletRotate(bullet, parent, firePos, lineNumber, angleDistance,  speed);
            if(clock)
            {
                angleDistance += 10;
            }
            else
            {
                angleDistance -= 10;
            }
        }
    }


    /// <summary>
    /// 圆线性连续弹幕生成方法
    /// </summary>
    /// <param name="bullet">子弹预制体</param>
    /// <param name="parent">子弹父类</param>
    /// <param name="lineNumber">生成圆线的数量</param>
    /// <param name="angleDistance">其实的角度</param>
    /// <param name="clock">顺时针还是逆时针，为真是逆时针</param>
    /// <param name="speed">发射速度</param>
    /// <param name="watiSceond">发射间隔时间</param>
    /// <returns></returns>
    public int StartCycleLines(GameObject bullet, GameObject parent, int lineNumber = 6,
     float angleDistance = 10, bool clock = true, float speed = 10.0f, float watiSceond = 0.2f)
    {
        Coroutine temp = StartCoroutine(CycleLinesShotHelp(bullet, parent,  lineNumber,
            angleDistance, clock, speed, watiSceond));

        int index = GetIndex(temp);
        return index;
    }





    //-------------------------------------------------废弃方法--------------------------------------------------------

    //private Dictionary<int, Coroutine> moreLineDictionary;
    //private Dictionary<int, Coroutine> eulerDictionary;
    //private Dictionary<int, Coroutine> cycleDictionary;
    //private Dictionary<int, Coroutine> cycleBoomDictionary;
    //private Dictionary<int, Coroutine> rotationLinesDictionary;
    //private Dictionary<int, Coroutine> continusRotationLinesDictionary;
    //private Dictionary<int, Coroutine> cycleLineDictionary;


    //eulerDictionary = new Dictionary<int, Coroutine>();
    //cycleDictionary = new Dictionary<int, Coroutine>();
    //cycleBoomDictionary = new Dictionary<int, Coroutine>();
    //rotationLinesDictionary = new Dictionary<int, Coroutine>();
    //continusRotationLinesDictionary = new Dictionary<int, Coroutine>();
    //cycleLineDictionary = new Dictionary<int, Coroutine>();
    //moreLineDictionary = new Dictionary<int, Coroutine>();

    //-------------------------------------------------协程索引获取方法--------------------------------------------------------

    //private int GetCycleLinesDictionaryIndex(Coroutine coroutine)
    //{

    //    int index = cycleLineDictionary.Count;
    //    while (cycleLineDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    cycleLineDictionary[index] = coroutine;
    //    return index;


    //}

    //private int GetContinusRotationLinesDictionaryIndex(Coroutine coroutine)
    //{

    //    int index = continusRotationLinesDictionary.Count;
    //    while (continusRotationLinesDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    continusRotationLinesDictionary[index] = coroutine;
    //    return index;


    //}

    //private int GetRotationLinesDictionaryIndex(Coroutine coroutine)
    //{

    //    int index = rotationLinesDictionary.Count;
    //    while (rotationLinesDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    rotationLinesDictionary[index] = coroutine;
    //    return index;


    //}

    //private int GetCycleDictionaryIndex(Coroutine coroutine)
    //{

    //    int index = cycleDictionary.Count;
    //    while (cycleDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    cycleDictionary[index] = coroutine;
    //    return index;


    //}

    //private int GetEulerDictionaryIndex(Coroutine coroutine)
    //{

    //    int index = eulerDictionary.Count;
    //    while (eulerDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    eulerDictionary[index] = coroutine;
    //    return index;


    //}


    //// 给协程一个索引
    //private int GetCycleBoomDictionaryIndex(Coroutine coroutine)
    //{

    //    int index = cycleBoomDictionary.Count;
    //    while (cycleBoomDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    cycleBoomDictionary[index] = coroutine;
    //    return index;


    //}



    //private int GetMoreLineDictionaryIndex(Coroutine coroutine)
    //{
    //    int index = moreLineDictionary.Count;
    //    while (moreLineDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    moreLineDictionary[index] = coroutine;

    //    return index;

    //}



    //// -----------------------------------------------------根据索引结束协程-----------------------------------------------

    //// 根据索引结束协程
    //public void StopCycleLines(int index)
    //{
    //    StopCoroutine(cycleLineDictionary[index]);
    //    cycleLineDictionary.Remove(index);
    //}

    // 根据索引结束协程
    //public void StopRotationLines(int index)
    //{
    //    StopCoroutine(rotationLinesDictionary[index]);
    //    rotationLinesDictionary.Remove(index);
    //}










    //// 根据索引结束协程
    //public void StopCycleBoom(int index)
    //{
    //    StopCoroutine(cycleBoomDictionary[index]);
    //    cycleBoomDictionary.Remove(index);
    //}


    //// 根据索引结束协程
    //public void StopMoreRotationLines(int index)
    //{
    //    StopCoroutine(continusRotationLinesDictionary[index]);
    //    continusRotationLinesDictionary.Remove(index);
    //}










    //// 根据索引结束协程
    //public void StopCycle(int index)
    //{
    //    StopCoroutine(cycleDictionary[index]);
    //    cycleDictionary.Remove(index);
    //}


    //// 根据索引结束协程
    //public void StopMoreLine(int index)
    //{
    //    StopCoroutine(moreLineDictionary[index]);
    //    moreLineDictionary.Remove(index);
    //}


    //// 根据索引结束协程
    //public void StopEuler(int index)
    //{
    //    StopCoroutine(eulerDictionary[index]);
    //    eulerDictionary.Remove(index);
    //}






    //// -------------------------------------------------废弃方法-------------------------------------------------------
    ////-------------------------------------------------螺旋形弹幕生成方法--------------------------------------------------------
    //private Dictionary<int, Coroutine> oneLineDictionary;
    //private Dictionary<int, Coroutine> rotationLineDictionary;
    //oneLineDictionary = new Dictionary<int, Coroutine>();
    //rotationLineDictionary = new Dictionary<int, Coroutine>();
    //// radius是弹幕的更新版基金，angleDistance是每次旋转的角度
    //private IEnumerator RotationLineShotHelp(GameObject bullet, GameObject parent,
    //    float radius, float angleDistance, float speed, float watiSceond)
    //{
    //    float rotateAngle = 0.0f;
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(watiSceond);
    //        rotateAngle = (rotateAngle / 360) * 2 * Mathf.PI;
    //        GameObject temp = CreateOneBullet(bullet, firePos);
    //        float offsetX = radius * Mathf.Cos(rotateAngle);
    //        float offsetY = radius * Mathf.Sin(rotateAngle);

    //        Vector3 targetPos = new Vector3(offsetX, offsetY, 0.0f);

    //        InitBullet(temp, parent, targetPos, speed);
    //        rotateAngle = (rotateAngle / (2 * Mathf.PI)) * 360;
    //        rotateAngle += angleDistance;
    //    }
    //}


    //public int ShotRotationLineStart(GameObject bullet, GameObject parent,
    //    float radius = 10, float angleDistance = 10, float speed = 4, float watiSceond = 0.1f)
    //{
    //    Coroutine temp = StartCoroutine(RotationLineShotHelp(bullet, parent,
    //        radius, angleDistance, speed, watiSceond));

    //    int index = GetRotationLineDictionaryIndex(temp);
    //    return index;
    //}


    //// 给协程一个索引
    //private int GetRotationLineDictionaryIndex(Coroutine coroutine)
    //{

    //    int index = rotationLineDictionary.Count;
    //    while (rotationLineDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    rotationLineDictionary[index] = coroutine;
    //    return index;


    //}


    //// 根据索引结束协程
    //public void StopRotationLineCoroutineWithIndex(int index)
    //{
    //    StopCoroutine(rotationLineDictionary[index]);
    //    rotationLineDictionary.Remove(index);
    //}

    ////-------------------------------------------------单行弹幕生成方法--------------------------------------------------------


    //private IEnumerator OneLineShotHelp(GameObject bullet, GameObject parent, Vector3 target,
    //    float speed, float waitSeconds)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(waitSeconds);
    //        GameObject temp = CreateOneBullet(bullet, firePos);
    //        target -= firePos;
    //        InitBullet(temp, parent, target, speed);
    //    }
    //}


    //public int ShotOneLineStart(GameObject bullet, GameObject parent, Vector3 target,
    //    float speed = 100f, float waitSeconds = 0.5f)
    //{
    //    Coroutine temp = StartCoroutine(OneLineShotHelp(bullet, parent, target, speed, waitSeconds));

    //    int index = GetOneLineDictionaryIndex(temp);
    //    return index;

    //}


    //// 给协程一个索引
    //private int GetOneLineDictionaryIndex(Coroutine coroutine)
    //{
    //    int index = oneLineDictionary.Count;
    //    while (oneLineDictionary.ContainsKey(index))
    //    {
    //        index++;
    //    }
    //    oneLineDictionary[index] = coroutine;

    //    return index;

    //}


    //// 根据索引结束协程
    //public void StopOneLineCoroutineWithIndex(int index)
    //{
    //    StopCoroutine(oneLineDictionary[index]);
    //    oneLineDictionary.Remove(index);
    //}

}
