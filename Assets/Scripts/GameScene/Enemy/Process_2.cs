using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Process_2 : MonoBehaviour
{
    // 由于会实例化飞船所以需要获取预制体
    private GameObject ship_2;
    private GameObject ships;

    GameObject[] ship_2s;

    //标准生成点
    private Vector3 pos;
    //标准旋转度
    private Vector3 rotate;



    private void Start()
    {
        ship_2s = new GameObject[3];
        rotate = new Vector3(0.0f, -90f, 90f);
        pos = new Vector3(300, 30, 260);

        ship_2 = Resources.Load<GameObject>("Enemy/Ship_2");
        ships = GameObject.Find("GameProcess");


        ship_2s[0] = GameObject.Instantiate(ship_2, pos, Quaternion.Euler(rotate), ships.transform);
        ship_2s[1] = GameObject.Instantiate(ship_2, pos + new Vector3(100,75,0.0f), Quaternion.Euler(rotate), ships.transform);
        ship_2s[2] = GameObject.Instantiate(ship_2, pos + new Vector3(100,-75, 0.0f), Quaternion.Euler(rotate), ships.transform);


        StartCoroutine(StartAct());
    }

    // y -100,100
    // x 300, 150


    // 全部的行为
    private IEnumerator StartAct()
    {
        for(int i = 0; i < 3;i++)
        {
            Vector3 m_Pos = ship_2s[i].transform.position;
            ship_2s[i].GetComponent<EnemyActHelper>().StartLineMove(m_Pos + new Vector3(-200,0.0f,0.0f), 3.0f);
        }
        yield return new WaitForSeconds(5.0f);

        StartCoroutine(StartTrianle(60f));

        yield return new WaitForSeconds(70.0f);
        for (int i = 0; i < 3; i++)
        {
            ship_2s[i].GetComponent<EnemyActHelper>().StartLineMove(pos, 3.0f);
        }
        yield return new WaitForSeconds(6.0f);
        for (int i = 0; i < 3; i++)
        {
            GameObject.Destroy(ship_2s[i]);
        }
        gameObject.SetActive(false);

    }





    private IEnumerator StartTrianle(float totalTime)
    {
        for (int i = 0; i < 3; i++)
        {
            ship_2s[i].GetComponent<ShipTwo>().StartCycle(totalTime);
            ship_2s[i].GetComponent<EnemyActHelper>().StartLineMove(ship_2s[(i+1)%3].transform.position, 3.0f);
        }
        yield return new WaitForSeconds(6.0f);
        float time = 6.0f;
        for (int i = 0; i < 3; i++)
        {
            ship_2s[i].GetComponent<ShipTwo>().StartEuler(totalTime);
        }
        while (time < totalTime)
        {
            for (int i = 0; i < 3; i++)
            {
                ship_2s[i].GetComponent<EnemyActHelper>().StartLineMove(ship_2s[(i + 1) % 3].transform.position, 3.0f);
            }
            yield return new WaitForSeconds(3.0f);
            time += 3.0f;
        }
    }
}
