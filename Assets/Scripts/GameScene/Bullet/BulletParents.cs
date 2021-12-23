using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParents : MonoBehaviour
{

    private Transform m_Transform;

    private void Start()
    {
        m_Transform = gameObject.transform;
        GameObject.Destroy(gameObject, 10.0f);
    }

    private void Update()
    {
        SetBulletPosZ(m_Transform);
    }

    protected void SetBulletPosZ(Transform m_Transform)
    {
        m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y,260);
    }
}
