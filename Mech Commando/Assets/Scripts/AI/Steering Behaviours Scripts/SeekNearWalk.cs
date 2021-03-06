﻿using System.Collections;
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
        direction = target.position - npc.position;
        direction.y = 0;
        lookDir = direction;
        lookDir.y = 0;

        if (AuxMethods.CompareDistanceSmaller(target.position, npc.position, minDistance)) //distance > minDistance
        {
            //lookDir = direction;
        }
        else if (AuxMethods.CompareDistanceEqual(target.position, npc.position, minDistance)) //distance == minDistance
        {
            direction = Vector3.zero;
        }
        else
        {
            direction = -direction;
            //lookDir = direction;
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
        if (Physics.Raycast(npc.position + Vector3.up * 10, Vector3.down, out hit, 1000, layerMask))
        {
         //  Debug.DrawRay(npc.position + Vector3.up * 10, Vector3.down * hit.distance, Color.yellow);
           // altitude = hit.point.y + hit.distance;
            altitude = hit.point.y;
        }
        else
        {
          //  Debug.DrawRay(npc.position + Vector3.up * 10, Vector3.down * 1000, Color.blue);
            altitude = 0;
        }

        return altitude;
    }
}
