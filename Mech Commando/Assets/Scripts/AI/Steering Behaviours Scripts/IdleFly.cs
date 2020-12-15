using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/IdleFly")]
public class IdleFly : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;
    [SerializeField]
    float flyAltitude = 10;

    public override Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        float currentAltitude = getFlyAltitude(npc) + flyAltitude; //Compute Altitute it neets to be at
        Vector3 t = new Vector3(npc.position.x,currentAltitude, npc.position.z) - npc.position; //compute direction so it can fly
        Vector3 lookDir = Vector3.zero;
        Vector3 dir = t;     



        Steering steering = new Steering();
        steering.linear = dir * maxAccel;
        steering.dir = lookDir;

        return steering;
    }

    float getFlyAltitude(MovementInfo npc)
    {
        float altitude = 0;
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(npc.position, Vector3.down, out hit, 1000, layerMask))
        {
            Debug.DrawRay(npc.position, Vector3.down * hit.distance, Color.yellow);
            altitude = hit.point.y;
        }
        else
        {
            Debug.DrawRay(npc.position, Vector3.down * 1000, Color.white);

        }

        return altitude;
    }
}
