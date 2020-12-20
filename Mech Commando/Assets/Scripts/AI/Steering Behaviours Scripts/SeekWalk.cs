using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/SeekWalk")]
public class SeekWalk : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;
    override public Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        // Direction Vector, From npc to target
        target.position.y = getWalkAltitude(npc);
        Vector3 direction = target.position - npc.position;
        Vector3 lookDir = direction;
        //  direction.y = -1;
        //direction.Normalize();


        Steering steering = new Steering();
        steering.linear = direction * maxAccel;
        steering.dir = lookDir;

        return steering;
    }


    float getWalkAltitude(MovementInfo npc)
    {
        float altitude = 0;

        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(npc.position, Vector3.down, out hit, 1000, layerMask))
        {
          //  Debug.DrawRay(npc.position, Vector3.down * hit.distance, Color.yellow);
            altitude = hit.point.y + hit.distance;
        }
        else
        {
          //  Debug.DrawRay(npc.position, Vector3.down * 1000, Color.white);

        }

        return altitude;
    }
}
