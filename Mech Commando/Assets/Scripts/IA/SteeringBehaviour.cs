using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class SteeringBehaviour : ScriptableObject
{
    public virtual SteeringBehaviour Init()
    {
        return this;
    }

    public abstract Steering GetSteering(MovementInfo npc, MovementInfo target);
}
