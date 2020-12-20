using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Idle")]
public class Idle : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;

    public override Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        Vector3 lookDir = Vector3.zero;
        Vector3 dir = Vector3.zero;
      //  dir.y = -1;



        Steering steering = new Steering();
        steering.linear = dir * maxAccel;
        steering.dir = lookDir;

        return steering;
    }


}
