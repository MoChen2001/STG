using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process_1 : MonoBehaviour
{



    // 由于会实例化飞船所以需要获取预制体
    private GameObject ship_1;
    private GameObject ships;

    //标准生成点
    private Vector3 pos;
    //标准旋转度
    private Vector3 rotate;



    private void Start()
    {

        rotate = new Vector3(0.0f,-90f,90f);
        pos = new Vector3(300, 0, 260);

        ship_1 = Resources.Load<GameObject>("Enemy/Ship_1");
        ships = GameObject.Find("GameProcess");




        StartCoroutine(StartAct());
    }


    /// <summary>
    /// 开始游戏流程
    /// </summary>
    private IEnumerator StartAct()
    {
        StartCoroutine(FirstAttack(10));
        yield return new WaitForSeconds(15);
        StartCoroutine(SecondAttack(30));
        yield return new WaitForSeconds(25);
        gameObject.SetActive(false);

    }



    // y -100,100
    // x 300, 150

    /// <summary>
    /// 第一波攻击
    /// </summary>
    /// <param name="totalTime">第一波攻击持续时间</param>
    /// <returns></returns>
    private IEnumerator FirstAttack(float totalTime)
    {
        float offset = 0;
        GameObject[] temps = new GameObject[7];
        Vector3 temp_Pos = new Vector3(pos.x, pos.y + offset, 260);
        temps[0] = GameObject.Instantiate(ship_1, temp_Pos, Quaternion.Euler(rotate), ships.transform);
        temps[0].GetComponent<ShipOne>().LinePushLineShotAttack(true);
        for (int i = 1; i <= 3; i++)
        {
            offset += 100 / 3;
            temp_Pos = new Vector3(pos.x, pos.y + offset, 260);
            temps[2 * i -1] = GameObject.Instantiate(ship_1, temp_Pos, Quaternion.Euler(rotate), ships.transform);
            temps[2 * i - 1].GetComponent<ShipOne>().LinePushLineShotAttack(true);
            temp_Pos = new Vector3(pos.x, pos.y - offset, 260);
            temps[2 * i] = GameObject.Instantiate(ship_1, temp_Pos, Quaternion.Euler(rotate), ships.transform);
            temps[2 * i].GetComponent<ShipOne>().LinePushLineShotAttack(true);
            yield return new WaitForSeconds(2.0f);
        }

        yield return new WaitForSeconds(totalTime);


        for(int i = 0; i < temps.Length;i++)
        {
            if(temps[i].active)
            {
                if(i % 2 == 0)
                {

                    Vector3 temp = new Vector3(150f,-200f,260f);
                    temps[i].GetComponent<EnemyActHelper>().StartLineMove(temp, 2.0f);
                    GameObject.Destroy(temps[i],3.0f);
                }
                else
                {
                    Vector3 temp = new Vector3(150f,200f,260f);
                    temps[i].GetComponent<EnemyActHelper>().StartLineMove(temp, 2.0f);
                    GameObject.Destroy(temps[i], 3.0f);
                }
            }
            else
            {
                GameObject.Destroy(temps[i]);
            }
        }
    }



    private IEnumerator SecondAttack(float totalTime)
    {
        float time = 0.0f;
        while(time < totalTime)
        {
            int offset = 0;
            GameObject[] temps = new GameObject[20];
            int count = 0;
            for(int i = 1; i <= 2; i++)
            {
                int offsetX = 150;
                for (int j = 1; j <= 5; j++)
                {
                    Vector3 offset_Y = new Vector3(0.0f, 100 - offset, 0.0f);
                    temps[count] = GameObject.Instantiate(ship_1,pos+ offset_Y, Quaternion.Euler(rotate), ships.transform);
                    temps[count].GetComponent<ShipOne>().NinthLinePushLineShotAttack(offsetX,false);
                    count = count +1;
                    temps[count] = GameObject.Instantiate(ship_1, pos- offset_Y, Quaternion.Euler(rotate), ships.transform);
                    temps[count].GetComponent<ShipOne>().NinthLinePushLineShotAttack(offsetX,false);
                    yield return new WaitForSeconds(0.1f);
                    count = count + 1;
                    offsetX += 20;
                }
                offset += 20;
            }
            yield return new WaitForSeconds(5.0f);
            for(int i = 0; i < temps.Length; i++)
            {
                if (temps[i].active)
                {
                    if (i % 2 == 0)
                    {

                        Vector3 temp = new Vector3(temps[i].transform.position.x, -200f, 260f);
                        temps[i].GetComponent<EnemyActHelper>().StartLineMove(temp, 8.0f);
                        GameObject.Destroy(temps[i], 12.0f);
                    }
                    else
                    {
                        Vector3 temp = new Vector3(temps[i].transform.position.x, 200f, 260f);
                        temps[i].GetComponent<EnemyActHelper>().StartLineMove(temp, 8.0f);
                        GameObject.Destroy(temps[i], 12.0f);
                    }
                }
                else
                {
                    GameObject.Destroy(temps[i]);
                }
            }
            time += 15.0f;
            yield return new WaitForSeconds(5.0f);
        }
    }


}
