using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipTwo : Ships
{
    private BulletShotHelper m_Shot;
    private EnemyActHelper m_Move;

    private Transform playerTrasform;



    // y -100,100
    // x 300, 150
    private void Awake()
    {
        m_Move = gameObject.GetComponent<EnemyActHelper>();
        m_Shot = gameObject.GetComponent<BulletShotHelper>();

        playerTrasform = GameObject.FindGameObjectWithTag("playerMesh").transform;

    }

    public void StartCycle(float totalTime)
    {
        StartCoroutine(StartCycleMoveAndShot(totalTime));
    }


    private IEnumerator StartCycleMoveAndShot(float totalTime)
    {
        float time = 0.0f;
        while(time < totalTime)
        {
            int index = m_Shot.StartCycleWithTimeOffsetAndTrace(Bullet_Common, Bullets,playerTrasform.position,
                BaseNumber * 3,BaseSpeed, BaseSpeed * 2, 0.8f, 0.5f);
            yield return new WaitForSeconds(2.0f);
            m_Shot.StopCoroutineWithIndex(index);
            time += 2.0f;
        }
    }


    public void StartEuler(float totalTime)
    {
        StartCoroutine(StartEulerShot(totalTime));
    }



    private IEnumerator StartEulerShot(float totalTime)
    {
        float time = 0.0f;
        while(time < totalTime)
        {
            int index = m_Shot.StartEuler(Bullet_Common, Bullets, BaseNumber * 3, 60, -BaseSpeed, 0.5f);
            yield return new WaitForSeconds(2.0f);
            m_Shot.StopCoroutineWithIndex(index);
            time += 2.0f;
        }
    }

}
