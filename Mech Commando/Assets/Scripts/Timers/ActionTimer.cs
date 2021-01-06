using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTimer : Timer
{
    public ButtonBehaviour behaviour;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    protected override void End()
    {
        base.End();
        behaviour.Run();
     
    }


    protected override void Run()
    {
        base.Run();

    }
}
