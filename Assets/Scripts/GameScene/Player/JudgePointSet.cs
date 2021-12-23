using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgePointSet : MonoBehaviour
{
    private Transform transform;

    private GameObject fireEffect;
    private GameObject effects;

    private void Start()
    {
        transform = GameObject.FindWithTag("MainCamera").transform;
        fireEffect = Resources.Load<GameObject>("Effects/Explosion");
        effects = GameObject.Find("Effects");
    }

    private void Update()
    {
        gameObject.transform.LookAt(transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet_Enemy")
        {
            Vector3 temp = new Vector3(0.0f, 0.0f, -20f);
            GameObject tempEff = GameObject.Instantiate(fireEffect, 
                other.transform.position - temp, Quaternion.identity, effects.transform);
            tempEff.AddComponent<EffectContro>();

            gameObject.GetComponentInParent<ShipControl>().MinusLife();

            other.gameObject.SetActive(false);
            GameObject.Destroy(other.gameObject,3.0f);
        }
    }
}
