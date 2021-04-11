using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessControl : MonoBehaviour
{
    private GameObject[] process;


    private GameObject nowProcess;
    private int index;


    private void Start()
    {
        index = 0;
        process = new GameObject[4];

        process[0] = Resources.Load<GameObject>("Enemy/Process_1");
        process[2] = Resources.Load<GameObject>("Enemy/Process_2");
        process[1] = Resources.Load<GameObject>("Enemy/Ship_3");
        process[3] = Resources.Load<GameObject>("Enemy/Ship_4");
        nowProcess = GameObject.Instantiate(process[index]);
        index++;
    }

    private void Update()
    {
        if(!nowProcess.activeSelf)
        {
            nowProcess = GameObject.Instantiate(process[index]);
            index++;
        }
    }

}
