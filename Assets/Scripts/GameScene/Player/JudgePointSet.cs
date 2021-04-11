using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgePointSet : MonoBehaviour
{
    private Transform transform;

    private void Start()
    {
        transform = GameObject.FindWithTag("MainCamera").transform;
    }

    private void Update()
    {
        gameObject.transform.LookAt(transform);
    }
}
