using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParents : MonoBehaviour
{

    protected void SetBulletPosZ(Transform m_Transform)
    {
        m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y,260);
    }
}
