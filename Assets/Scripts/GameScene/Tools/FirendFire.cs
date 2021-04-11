using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirendFire : MonoBehaviour
{
    private Transform m_Transform;
    private BulletShotHelper m_shot;
    private int index;
    private Vector3 pos;

    private GameObject bullet;
    private GameObject bullets;


    private void Start()
    {

        m_Transform = gameObject.transform;
        pos = m_Transform.position + new Vector3(100, 0, 0);
        m_shot = gameObject.GetComponent<BulletShotHelper>();
        bullet = Resources.Load<GameObject>("Bullet/Bullet_Player_1");
        bullets = GameObject.Find("BulletParent");

        StartCoroutine(Haviour());
    }



    // 行为！
    private IEnumerator Haviour()
    {
        while (true)
        {
            m_Transform.position += new Vector3(1.0f, 00f, 0.0f);
            yield return new WaitForSeconds(0.01f);
            if (pos == m_Transform.position)
            {
                pos -= new Vector3(100, 0, 0);
                break;
            }
        }
        yield return new WaitForSeconds(0.1f);
        index = m_shot.StartEuler(bullet, bullets, 10, 120, 40, 0.2f);
        yield return new WaitForSeconds(5.0f);
        m_shot.StopCoroutineWithIndex(index);
        yield return new WaitForSeconds(0.1f);
        while(true)
        {
            m_Transform.position -= new Vector3(1.0f, 00f, 0.0f);
            yield return new WaitForSeconds(0.01f);
            if(pos == m_Transform.position)
            {
                break;
            }
        }
        yield return new WaitForSeconds(0.1f);
        GameObject.Destroy(gameObject);
    }
}
