using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/Seek")]
public class Seek : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;
    override public Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        // Direction Vector, From npc to target
        Vector3 direction = target.position - npc.position;

        //direction.Normalize();


        Steering steering = new Steering();
        steering.linear = direction * maxAccel;

        return steering;
    }
}
