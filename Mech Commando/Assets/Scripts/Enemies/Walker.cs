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



        // changeState(Walker_State.Idle);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        DTAction _SEEK = new DTAction(() => movementManager.selectCurrentBehaviour("SeekNear_Walker"));
        DTAction _IDLE = new DTAction(() => movementManager.selectCurrentBehaviour("Idle"));
        //DTAction _Shoot 
        //DTCondition _Shoot = new DTCondition(() => Vector3.Distance(info.position, currentTarget.transform.position) < 50, Disparar, _IDLE);
        DTCondition _distancia = new DTCondition(() => Vector3.Distance(info.position, currentTarget.transform.position) < 2000, _SEEK, _IDLE);
        _distancia.Tarefa();

        // checkDistanceToTarget();

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
