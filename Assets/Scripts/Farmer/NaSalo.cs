using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaSalo : MonoBehaviour
{
    public GameObject pig;
    public GameObject curtain;
    public Restart restart;
    public void FinishHim()
    {
        restart.curtain = Instantiate(curtain);
        Transform curtainTranform = restart.curtain.transform;
        curtainTranform.position = pig.transform.position;
    }
}
