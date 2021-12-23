using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithSelf : MonoBehaviour
{
    private GameObject fireEffect;
    private GameObject effects;



    void Awake()
    {
        fireEffect = Resources.Load<GameObject>("Effects/Explosion");
        effects = GameObject.Find("Effects");
    }


    public void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Bullet_Self")
        {
            Vector3 temp = new Vector3(0.0f, 0.0f, -20f);
            GameObject tempEff = GameObject.Instantiate(fireEffect, coll.transform.position - temp, Quaternion.identity, effects.transform);
            tempEff.AddComponent<EffectContro>();

            coll.gameObject.SetActive(false);
            GameObject.Destroy(coll.gameObject,2.0f);

            if (gameObject.tag == "enemy")
            {
                RewardParent.Instance.CreateStarNum(coll.transform.position);
                Ships ships = gameObject.GetComponentInParent<Ships>();
                ships.MinusLife();
            }

        }
    }
}
