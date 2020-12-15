using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walker : Enemy
{

    enum W_State
    {
        Idle,
        SeekNear,
        Moving,
        Patroling
    }
    W_State currentState;

    DTCondition initiateCon;
    DTCondition moveCon;

    protected override void Awake()
    {
        base.Awake();

        
        DTAction _ATTACK = new DTAction(() => changeState(W_State.SeekNear));
        DTAction _IDLE = new DTAction(() => changeState(W_State.Idle));
        DTAction _MOVE = new DTAction(() => changeState(W_State.Moving));
        //DTAction _Shoot 
        //DTCondition _Shoot = new DTCondition(() => Vector3.Distance(info.position, currentTarget.transform.position) < 50, Disparar, _IDLE);
        moveCon = new DTCondition(() => CheckForSeek(), _MOVE, _ATTACK);
        initiateCon = new DTCondition(() => checkForInitiation(), moveCon, _IDLE);

       // dtConditions.Add(attackPlayer);
    }

    bool checkForInitiation()
    {
        float distance = Vector3.Distance(info.position, player.position);
        return currentState == W_State.Idle && distance < radarRange;
    }

    bool CheckForSeek()
    {
        switch (currentState)
        {
            case W_State.SeekNear:
                return isPathObstructed(player);
            case W_State.Moving:
                return isPathObstructed(player);
            default:
                break;
        }

        return false;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
       // base.Update();

        if (currentState == W_State.Idle) initiateCon.Task();
        else
        {
            moveCon.Task();
        }


        movementManager.Run(currentTarget, info, speed);

    }

    public override void Die()
    {
        base.Die();


    }


    void changeState(W_State newState)
    {
        if (newState != currentState)
        {
            Debug.Log($"State of ${gameObject.name} AI changed from {currentState.ToString()} to {newState.ToString()}");
            currentState = newState;

            GetPathToTarget(currentState);

        }

        movementManager.selectCurrentBehaviour(GetStateBehaviour(currentState));
    }

    string GetStateBehaviour(W_State state)
    {
        string behaviour = "";
        switch (state)
        {
            case W_State.Idle:
                behaviour = "Idle";
                break;
            case W_State.SeekNear:
                behaviour = "SeekNear";
                break;
            case W_State.Moving:
                behaviour = "Seek";
                break;
            case W_State.Patroling:
                behaviour = "Seek";
                break;
            default:
                break;
        }
        return behaviour;
    }

    void GetPathToTarget(W_State state)
    {
        if (state == W_State.Moving || state == W_State.Patroling)
        {
            currentPath = manager.getPathToTarget(info, currentTarget);
            if (currentPath.Count > 0) currentTarget = currentPath[0].GetInfo;
            else currentTarget = manager.getPlayer().GetInfo;
        }
        else
        {
            currentTarget = player;
        }
    }

}
