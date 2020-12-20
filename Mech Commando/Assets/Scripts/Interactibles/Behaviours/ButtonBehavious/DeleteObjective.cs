using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(menuName = ("ButtonBehaviour/Delete Objective"))]
public class DeleteObjective : ButtonBehaviour
{

    bool active = true;
    ObjectiveManager manager;

    public override void Initialize()
    {
        manager = FindObjectOfType<ObjectiveManager>();

        active = true;
    }

    public override void Run()
    {
        if (active)
        {
            manager.DeleteObjective();
            active = false;
        }

    }
}
