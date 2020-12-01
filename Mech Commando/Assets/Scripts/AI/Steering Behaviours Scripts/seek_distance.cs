using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Behaviour/SeekNear")]
public class seekdistance : SteeringBehaviour
{
    [SerializeField]
    float maxAccel = 3f;

    override public Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        Steering steering;
        float dis = Vector3.Distance(target.position, npc.position);
            if (dis > 40)
            {
                // Direction vector, from npc to target
                Vector3 direction = target.position - npc.position;

                steering = new Steering();
                steering.linear = direction.normalized * maxAccel;
                steering.dir = direction;
            }
            else if (dis == 40)
            {

                Vector3 direction = target.position - npc.position;

                steering = new Steering();
                steering.linear = new Vector3(0,0,0);
                steering.dir = direction;
            }
            else 
            {

                Vector3 direction = target.position - npc.position;

                steering = new Steering();
                steering.linear = -direction.normalized * maxAccel;
                steering.dir = direction;
            }

        return steering;
    }
}
