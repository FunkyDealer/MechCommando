using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionObjective : MissionWayPoint
{

    protected override void Awake()
    {
        
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        manager.changeObjectiveText(objectiveText);
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }
}
