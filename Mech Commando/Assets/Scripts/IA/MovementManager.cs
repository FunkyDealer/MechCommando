using System;
using UnityEngine;

public class MovementManager : MonoBehaviour
{
    [SerializeField]
    MovementInfo info;
    [SerializeField]
    SteeringBehaviour steeringBehaviour;
    [SerializeField,Range(0,1)]
    float linearDrag = 0.95f, angularDrag = 0.95f;
    [SerializeField,Tooltip("NPC max velocity")]
    float maxVelocity = 2f;
    PlayerMovementManager pmm;
    MovementInfo target;
    public MovementInfo GetInfo => info;
    void Awake()
    {
        info.position = transform.position;
        Vector3 forward = transform.forward;
        info.orientation = Mathf.Atan2(forward.x, forward.z);
        
        steeringBehaviour = Instantiate(steeringBehaviour);
        steeringBehaviour.Init();
    }

    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerMovementManager>().GetInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
        info.position    += info.velocity * Time.deltaTime;
        info.orientation += info.rotation * Time.deltaTime;

     
        info.velocity *= linearDrag;
        info.rotation *= angularDrag;

        // Update da velocidade consoante o steering
        Steering steering = steeringBehaviour.GetSteering(info, target);
        info.velocity += steering.linear;
        info.rotation += steering.angular;

        // limitação da velcidade
        info.velocity = Vector3.ClampMagnitude(info.velocity, maxVelocity);

        // radianos para graus
        info.orientation = AuxMethods.NormAngle(info.orientation);
        transform.rotation = Quaternion.identity;
        transform.Rotate(transform.up, info.orientation * Mathf.Rad2Deg);
        transform.position = info.position;
    }
}
