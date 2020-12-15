using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = ("ButtonBehaviour/Spawn Timer"))]
public class SpawnTimer : ButtonBehaviour
{
    [SerializeField]
    GameObject timer2Spawn;

    Vector3 pos;

    [SerializeField]
    int timerLenght;
    [SerializeField]
    string timerText;


    public override void Initialize()
    {

    }


    public override void Run()
    {

        GameObject o = GameObject.Instantiate(timer2Spawn, pos, Quaternion.identity);
        Timer t = o.GetComponent<Timer>();

        t.totalSeconds = timerLenght;
        t.timerText = timerText;
    }
}
