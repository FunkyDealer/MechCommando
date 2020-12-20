using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/SeekNearWalk")]
public class SeekNearWalk : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;
    [SerializeField]
    float minDistance;

    override public Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        Steering steering;

        float y = getWalkAltitude(npc);
       // float dis = Vector3.Distance(target.position, npc.position);

        Vector3 direction;
        Vector3 lookDir;

        // Direction vector, from npc to target
        Vector3 t = new Vector3(target.position.x, y, target.position.z);
        direction = t - npc.position;
        direction.y = 0;
        lookDir = direction;

        if (AuxMethods.CompareDistanceSmaller(target.position, npc.position, minDistance))
        {
            lookDir = -direction;

        }
        else if (AuxMethods.CompareDistanceEqual(target.position, npc.position, minDistance))
        {
            direction = Vector3.zero;
        }
        else
        {
            direction = -direction;
            lookDir = direction;
        }


        // direction.y = -1;

        steering = new Steering();
        steering.linear = direction.normalized * maxAccel;
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
           // Debug.DrawRay(npc.position, Vector3.down * hit.distance, Color.yellow);
            altitude = hit.point.y + hit.distance;
        }
        else
        {
          //  Debug.DrawRay(npc.position, Vector3.down * 1000, Color.white);
            altitude = -100;
        }

        return altitude;
    }
}
