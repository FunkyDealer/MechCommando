using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/SeekFly")]
public class SeekFly : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;
    [SerializeField]
    float flyAltitude = 10f;

    override public Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        // Direction Vector, From npc to target
        target.position.y = getFlyAltitude(npc) + flyAltitude;
        Vector3 direction = target.position - npc.position;       
        Vector3 lookDir = direction;
        
        //direction.Normalize();


        Steering steering = new Steering();
        steering.linear = direction * maxAccel;
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
        Debug.Log(altitude);

        return altitude;
    }

}
