using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private ShipControl m_ShipControl;

    private bool canMove = true;

    
    private GameObject player;


    private void Awake()
    {
        Instance = this;
    }


    private void Update()
    {
        if(UIStateController.Instance.gameOver || UIStateController.Instance.gameParse)
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

        PlayerMoveInWindows();
    }


    private void PlayerMoveInWindows()
    {
        if(canMove)
        {
            if (Input.GetKey(KeyCode.S))
            {
                if (player.transform.position.y > -138.0f)
                {
                    player.transform.position += new Vector3(0, -GetMoveSpeedInWindows(), 0);
                }
            }
            if (Input.GetKey(KeyCode.W))
            {

                if (player.transform.position.y < 138.0f)
                {
                    player.transform.position += new Vector3(0, GetMoveSpeedInWindows(), 0);
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (player.transform.position.x < 250.0f)
                {
                    player.transform.position += new Vector3(GetMoveSpeedInWindows(), 0, 0);
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (player.transform.position.x > -260.0f)
                {
                    player.transform.position += new Vector3(-GetMoveSpeedInWindows(), 0, 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerAttack();
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                PlayerAttackEnd();
            }
        }
    }





    // 移动事件
    public void PlayerMove(Vector2 vec)
    {
        if(canMove)
        {
            Vector3 pos = Vector3.zero;

            // maxY = 138.0f minY = -138.0f
            // minX = -260.0f minX = 250.0f

            if (!((player.transform.position.x > 250.0f && vec.x > 0.0f) || (player.transform.position.x < -260.0f && vec.x < 0.0f)))
            {
                pos.x = vec.x;
            }
            if (!((player.transform.position.y > 138.0f && vec.y > 0.0f) || (player.transform.position.y < -138.0f && vec.y < 0.0f)))
            {
                pos.y = vec.y;
            }

            pos *= GetMoveSpeed();
            player.transform.position += pos;
            //PlayerMoveInWindows(vec);
            player.transform.LookAt(player.transform.position);
        }
    }




    private float GetMoveSpeedInWindows()
    {
        return m_ShipControl.GetSpeedInWindows();
    }

    /// <summary>
    ///  手机端的速度
    /// </summary>
    /// <returns></returns>
    private int GetMoveSpeed()
    {
        return m_ShipControl.GetSpeed();
    }

    /// 攻击事件
    public void PlayerAttack()
    {
        m_ShipControl.ShipShotStart();
    }


    // 结束攻击事件
    public void PlayerAttackEnd()
    {
        m_ShipControl.ShipShotEnd();
    }



    public void InitPlayer(string path)
    {
        GameObject temp = Resources.Load<GameObject>(path);
        Vector3 rotate = new Vector3(0.0f, 90f, -90f);
        Vector3 pos = new Vector3(-220, 0.0f, 260f);
        player = GameObject.Instantiate(temp, pos, Quaternion.Euler(rotate), gameObject.transform);
        m_ShipControl = player.AddComponent<ShipControl>();
        player.AddComponent<BulletShotHelper>();
    }
}
