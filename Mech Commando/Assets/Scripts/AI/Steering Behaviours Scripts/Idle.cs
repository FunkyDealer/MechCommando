using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Idle")]
public class Idle : SteeringBehaviour
{
    public override Steering GetSteering(MovementInfo npc, MovementInfo target)
    {


        Steering steering = new Steering();
        steering.linear = Vector3.zero;

        return steering;
    }


}
