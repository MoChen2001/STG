using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardParent : MonoBehaviour
{
    private GameObject reward;
    private GameObject starNum;

    public static RewardParent Instance;


    private void Start()
    {
        Instance = this;

        starNum = Resources.Load<GameObject>("Others/Star");
        reward = Resources.Load<GameObject>("Others/Reward");
        InvokeRepeating("CreateReward",10,20f);
    }


    private void CreateReward()
    {
        float random = Random.Range(-130.0f,130.0f);
        Vector3 pos = new Vector3(270, random, 260);
        GameObject temp = GameObject.Instantiate(reward, pos, Quaternion.identity, gameObject.transform);
        Vector3 target = new Vector3(Random.Range(-1,0), Random.Range(-1, 1), 0.0f);
        float speed = Random.Range(20, 50);
        temp.GetComponent<Rigidbody>().AddForce(target * speed,ForceMode.Impulse);
    }


    public void CreateStarNum(Vector3 pos)
    {
        Vector3 target = new Vector3(Random.Range(-1, 0), Random.Range(-1, 1), 0.0f);
        GameObject temp = GameObject.Instantiate(starNum, pos + target, Quaternion.identity, gameObject.transform);
        float speed = Random.Range(20, 50);
        temp.GetComponent<Rigidbody>().AddForce(target * speed, ForceMode.Impulse);
    }
}
