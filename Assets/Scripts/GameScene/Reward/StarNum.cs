using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarNum : MonoBehaviour
{
    private Transform m_Transform;

    private void Start()
    {
        m_Transform = gameObject.transform;
    }

    private void Update()
    {
        m_Transform.position = new Vector3(m_Transform.position.x, m_Transform.position.y, 265);

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "playerMesh")
        {
            UIStateController.Instance.AddNum();
            GameObject.Destroy(gameObject);
        }
        else if (other.tag == "wall")
        {
            GameObject.Destroy(gameObject.GetComponentInParent<Transform>().gameObject, 2.0f);
        }
    }
}
