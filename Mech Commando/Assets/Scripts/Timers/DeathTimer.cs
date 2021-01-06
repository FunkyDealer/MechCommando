using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : Timer
{
    Player player;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }


    protected override void End()
    {
        base.End();

        player.Die();
    }


    protected override void Run()
    {
        base.Run();

    }

}
