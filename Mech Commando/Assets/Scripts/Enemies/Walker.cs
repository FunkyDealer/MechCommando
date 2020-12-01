using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : Enemy
{

    enum Walker_State
    {
        Idle,
        SeekNear
    }
    Walker_State currentState;

    protected override void Awake()
    {
        base.Awake();

               
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        

        changeState(Walker_State.Idle);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


        checkDistanceToTarget();

        movementManager.Run(currentTarget.GetInfo, info, speed);
    }

    public override void Die()
    {
        base.Die();


    }

    void checkDistanceToTarget()
    {
        if (Vector3.Distance(transform.position, currentTarget.transform.position) <= radarRange)
        {
            changeState(Walker_State.SeekNear);


        }
    }

    void changeState(Walker_State newState)
    {
        currentState = newState;
        movementManager.selectCurrentBehaviour(currentState.ToString());
        Debug.Log($"State of ${gameObject.name} AI changed to {currentState.ToString()}");
    }

}
