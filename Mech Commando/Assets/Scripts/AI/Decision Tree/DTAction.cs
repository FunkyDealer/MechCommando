using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTAction : DTNode
{
    private Action action;

    public DTAction(Action action)
    {
        this.action = action;
    }

    public override void Task()
    {
        action();
    }
}
