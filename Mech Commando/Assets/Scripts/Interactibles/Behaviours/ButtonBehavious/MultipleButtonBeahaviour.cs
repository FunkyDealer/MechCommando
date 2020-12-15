using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenuAttribute(menuName = ("ButtonBehaviour/Multiple Behaviours"))]
public class MultipleButtonBeahaviour : ButtonBehaviour
{

    [SerializeField]
    List<ButtonBehaviour> behaviours;


    public override void Initialize()
    {
        foreach (var b in behaviours)
        {
            b.Initialize();
        }
    }


    public override void Run()
    {

        foreach (var b in behaviours)
        {
            b.Run();
        }

    }
}
