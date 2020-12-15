using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class DTCondition : DTNode
{
    private DTNode trueNode, falseNode;
    private Func<bool> condition;

    public DTCondition(Func<bool> condition, DTNode tChild, DTNode fChild)
    {
        this.condition = condition;
        trueNode = tChild;
        falseNode = fChild;
    }
    public override void Task()
    {
        if (condition())
        {
            trueNode.Task();
        }
        else
        {
            falseNode.Task();
        }
    }
}
