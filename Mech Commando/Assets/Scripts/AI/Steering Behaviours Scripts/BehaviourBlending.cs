using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = ("Behaviour/BehaviourBlending"))]
public class BehaviourBlending : SteeringBehaviour
{
    [SerializeField]
    List<BehaviourAndWeight> behaviours;

    [SerializeField]
    private float maxAcceleration;
    [SerializeField]
    private float maxRotation;

    public void OnEnable()
    {
        foreach (BehaviourAndWeight b in behaviours)
        {
            b.behaviour = Instantiate(b.behaviour);
        }
    }


    public override Steering GetSteering(MovementInfo npc, MovementInfo target)
    {
        Steering steering = new Steering();
        float totalWeights = 0f;

        foreach (BehaviourAndWeight behaviour in behaviours)
        {
            Steering tmp = behaviour.behaviour.GetSteering(npc, target);
            steering.linear += tmp.linear * behaviour.weight;
            steering.angular += tmp.angular * behaviour.weight;
            totalWeights += behaviour.weight;
        }

        //weighted average
        steering.angular /= totalWeights;
        steering.linear /= totalWeights;
        // nao ultrapassar limites de acelaração
        steering.linear = Vector3.ClampMagnitude(steering.linear, maxAcceleration);
        steering.angular = Mathf.Clamp(steering.angular, -maxRotation, maxRotation);

        return steering;
    }


}
