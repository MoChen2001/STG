using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : BulletParents
{
    private Transform m_Transform;
    private GameObject fireEffect;
    private GameObject effects;

    private void Start()
    {
        m_Transform = gameObject.transform;
        fireEffect = Resources.Load<GameObject>("Effects/Explosion");
        effects = GameObject.Find("Effects");
    }

    private void Update()
    {
        SetBulletPosZ(m_Transform);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "wall")
        {
            gameObject.SetActive(false);//= false;
            GameObject.Destroy(gameObject, 30f);
        }
        else if(other.tag == "player")
        {
            other.GetComponentInParent<ShipControl>().MinusLife();
            Vector3 temp = new Vector3(0.0f, 0.0f, -20f);
            GameObject tempEff = GameObject.Instantiate(fireEffect, other.transform.position - temp, Quaternion.identity, effects.transform);

            tempEff.AddComponent<EffectContro>();
            gameObject.SetActive(false);//= false;
            GameObject.Destroy(gameObject,30f);
        }
    }

}
