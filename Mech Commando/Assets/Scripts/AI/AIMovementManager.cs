using System;
using System.Collections.Generic;
using UnityEngine;

public class AIMovementManager : MonoBehaviour
{
    [SerializeField]
    List<SteeringBehaviour> behaviourList;

    SteeringBehaviour currentSteeringBehaviour;
    [SerializeField,Range(0,1)]
    float linearDrag = 0.95f, angularDrag = 0.95f;
    string currentBehaviour;

    CharacterController controller;
    
    void Awake()
    {
        currentBehaviour = null;
        controller = GetComponent<CharacterController>();
        
    }

    void Start()
    {

    }

    // Update is called once per frame
    public void Run(MovementInfo target, MovementInfo info, float maxVelocity)
    {
      

      Move(target, info, maxVelocity);

    }

    void Move(MovementInfo target, MovementInfo AiInfo, float maxVelocity)
    {
        AiInfo.position += AiInfo.velocity * Time.deltaTime;
        AiInfo.orientation += AiInfo.orientation * Time.deltaTime;

        AiInfo.velocity *= linearDrag;
        AiInfo.rotation *= angularDrag;

        // Update Velocity from steering
        Steering steering = currentSteeringBehaviour.GetSteering(AiInfo, target);
        AiInfo.velocity += steering.linear;
        AiInfo.rotation += steering.angular;

        // Velocity Limiter
        AiInfo.velocity = Vector3.ClampMagnitude(AiInfo.velocity, maxVelocity);

        // Radians to dregrees
        AiInfo.orientation = AuxMethods.NormAngle(AiInfo.orientation);
        transform.rotation = Quaternion.identity;
        //transform.Rotate(transform.up, AiInfo.orientation * Mathf.Rad2Deg);
        if (steering.dir != Vector3.zero) transform.forward = steering.dir;

        //  transform.position = info.position;
        transform.position += AiInfo.velocity * Time.deltaTime;


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
