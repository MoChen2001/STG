using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    private Transform transform;
    private Vector3 aixs;

    void Start()
    {
        transform = gameObject.transform;
        transform.position = new Vector3(-3.207502f, 1.069167f, -24.14838f);
        aixs = new Vector3(0.0f, 0.0f, transform.position.z);
    }

    
    void Update()
    {
        gameObject.transform.Rotate(aixs, Time.deltaTime * 10.0f);
    }
}
