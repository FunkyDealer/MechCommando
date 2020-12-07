using System;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementManager : MonoBehaviour
{
    [SerializeField]
    public List<SteeringBehaviour> behaviourList;
    //SteeringBehaviour steeringBehaviour;
    
    SteeringBehaviour currentSteeringBehaviour;
    [SerializeField, Range(0, 1)]
    float linearDrag = 0.95f, angularDrag = 0.95f;
    string currentBehaviour;

    void Awake()
    {
        currentBehaviour = null;

    }

    void Start()
    {

    }

    // Update is called once per frame
    public void Run(MovementInfo target, MovementInfo info, float maxVelocity)
    {
        info.position += info.velocity * Time.deltaTime;
        info.orientation += info.rotation * Time.deltaTime;

        info.velocity *= linearDrag;
        info.rotation *= angularDrag;

        // Update da velocidade consoante o steering
        Steering steering = currentSteeringBehaviour.GetSteering(info, target);
        info.velocity += steering.linear;
        info.rotation += steering.angular;

        // limitação da velcidade
        info.velocity = Vector3.ClampMagnitude(info.velocity, maxVelocity);

        // radianos para graus
        info.orientation = AuxMethods.NormAngle(info.orientation);
        transform.rotation = Quaternion.identity;
        //  transform.Rotate(transform.up, info.orientation * Mathf.Rad2Deg);
        transform.forward = -steering.dir;
        transform.position = info.position;
    }

    public void selectCurrentBehaviour(string behaviour)
    {
        if (currentBehaviour != behaviour)
        {
            foreach (var b in behaviourList)
            {
                if (b.Name == behaviour)
                {
                    this.currentSteeringBehaviour = b;
                    currentBehaviour = behaviour;
                }
            }


            currentSteeringBehaviour = Instantiate(currentSteeringBehaviour);
            currentSteeringBehaviour.Init();
        }
    }
}
