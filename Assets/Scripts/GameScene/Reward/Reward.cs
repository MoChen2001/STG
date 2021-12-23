using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    private Transform m_Transform;

    private void Start()
    {
        m_Transform = gameObject.transform;
    }

    private void Update()
    {
        m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y, 260);

    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "playerMesh")
        {
            ShipControl temp = other.GetComponentInParent<ShipControl>();
            if(temp != null)
            {
                temp.AddShotPoint();
            }
            gameObject.GetComponentInParent<Transform>().gameObject.SetActive(false);
            GameObject.Destroy(gameObject.GetComponentInParent<Transform>().gameObject);
        }
        else if(other.tag == "wall")
        {
            GameObject.Destroy(gameObject.GetComponentInParent<Transform>().gameObject, 2.0f);
        }
    }
}
