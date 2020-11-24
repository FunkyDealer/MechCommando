using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Seek")]
public class Seek : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;
    [SerializeField]
    float range = 100;
    override public Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        Steering steering;

        if (range > Vector3.Distance(target.position, npc.position))
        {
            // Direction vector, from npc to target
            Vector3 direction = target.position - npc.position;

            steering = new Steering();
            steering.linear = direction.normalized * maxAccel;
        }
        else
        {
            steering = new Steering();
        }

        return steering;
    }
}
