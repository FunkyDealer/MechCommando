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
       // MoveController(target, info, maxVelocity);

      MoveByHand(target, info, maxVelocity);

    }

    void MoveByHand(MovementInfo target, MovementInfo AiInfo, float maxVelocity)
    {
        AiInfo.position += AiInfo.velocity * Time.deltaTime;
        AiInfo.orientation += AiInfo.rotation * Time.deltaTime;

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
        //  transform.Rotate(transform.up, info.orientation * Mathf.Rad2Deg);
        transform.forward = -steering.dir;
        //  transform.position = info.position;
        transform.position += AiInfo.velocity * Time.deltaTime;


    }

    void MoveController(MovementInfo target, MovementInfo info, float maxVelocity)
    {
        info.orientation += info.rotation * Time.deltaTime;

        info.velocity *= linearDrag;
        info.rotation *= angularDrag;

        // Update Velocity from steering
        Steering steering = currentSteeringBehaviour.GetSteering(info, target);
        info.velocity += steering.linear;
        info.rotation += steering.angular;

        //Controlers
        controller.Move(new Vector3(
        info.velocity.x * Time.deltaTime,
        0,
        info.velocity.z * Time.deltaTime
        ));

            //Gravity
            controller.Move(new Vector3(
            0,
            info.velocity.y * Time.deltaTime,
            0));

        info.position = transform.position;

        info.velocity = Vector3.ClampMagnitude(info.velocity, maxVelocity);

        // Radians to dregrees
        info.orientation = AuxMethods.NormAngle(info.orientation);

        transform.rotation = Quaternion.identity;
        transform.forward = -steering.dir;
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
