using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : BulletParents
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



    private void OnTriggerEnter(Collider coll)
    {
        if(coll.tag  == "wall")
        {
            GameObject.Destroy(gameObject);
        }
        if(coll.tag == "enemy")
        {
            RewardParent.Instance.CreateStarNum(coll.transform.position);
            Vector3 temp = new Vector3(0.0f, 0.0f, -20f);
            GameObject tempEff = GameObject.Instantiate(fireEffect, coll.transform.position - temp, Quaternion.identity, effects.transform);
            tempEff.AddComponent<EffectContro>();
            Ships ships = coll.GetComponentInParent<Ships>();
            ships.MinusLife();
            GameObject.Destroy(gameObject);
        }
        if(coll.tag == "boss")
        {
            Vector3 temp = new Vector3(0.0f, 0.0f, -20f);
            GameObject tempEff = GameObject.Instantiate(fireEffect, coll.transform.position - temp, Quaternion.identity, effects.transform);
            tempEff.AddComponent<EffectContro>();
            GameObject.Destroy(gameObject);

        }
    }
}
