using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideDrone : Enemy
{

    enum SD_State
    {
        Idle,
        Seek,
        Charge,
        Moving,
        Patroling
    }
    [SerializeField]
    SD_State currentState;

    [SerializeField]
    float chargeDis;

    [SerializeField]
    float ExplosionDis;

    DTCondition initiateCon;
    DTCondition chargeCon;
    DTCondition explodeCon;
    DTCondition moveCon;


    protected override void Awake()
    {
        base.Awake();

        DTAction _SEEK = new DTAction(() => changeState(SD_State.Seek));
        DTAction _CHARGE = new DTAction(() => changeState(SD_State.Charge));
        DTAction _IDLE = new DTAction(() => changeState(SD_State.Idle));
        DTAction _MOVEINTOPOSITION = new DTAction(() => changeState(SD_State.Moving));
        DTAction _EXPLODE = new DTAction(() => Explode());


        initiateCon = new DTCondition(() => checkForInitiation(), _SEEK, _IDLE);

        moveCon = new DTCondition(() => checkForSeek(), _MOVEINTOPOSITION, _SEEK);

        chargeCon = new DTCondition(() => checkForCharge(), _CHARGE, moveCon);

        explodeCon = new DTCondition(() => checkForExplosion(), _EXPLODE, chargeCon);

    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        

        
        currentState = SD_State.Idle;
    }


    bool checkForInitiation()
    {
        //float distance = Vector3.Distance(info.position, player.position);
        return currentState == SD_State.Idle && AuxMethods.CompareDistanceBigger(info.position, player.position, radarRange);
    }

    bool checkForSeek()
    {
        switch (currentState)
        {
            case SD_State.Seek:
                return isPathObstructed(player);
            case SD_State.Charge:
                break;
            case SD_State.Moving:
                return isPathObstructed(player);
            case SD_State.Patroling:
                break;
            default:          
                break;
        }
        
        return false;
    }

    bool checkForCharge()
    {
        if (currentState == SD_State.Seek || currentState == SD_State.Charge)
        {
            float distance = Vector3.Distance(info.position, player.position);
            return AuxMethods.CompareDistanceBigger(info.position, player.position, chargeDis) && !isPathObstructed(player); 
        }
        else return false;
    }

    bool checkForExplosion()
    {
        if (currentState == SD_State.Charge)
        {
            float distance = Vector3.Distance(info.position, player.position);
            return AuxMethods.CompareDistanceBigger(info.position, player.position, ExplosionDis) && !isPathObstructed(player); 
        }
        else return false;
    }



    // Update is called once per frame
    protected override void Update()
    {
        //base.Update();

        if (currentState == SD_State.Idle) initiateCon.Task();
        else
        {
            //if (currentState != SD_State.Charge)
            moveCon.Task();
            chargeCon.Task();
            explodeCon.Task();
        }


        movementManager.Run(currentTarget, info, speed);
    }

    public override void Die()
    {
        base.Die();


    }

    void changeState(SD_State newState)
    {
        if (newState != currentState)
        {
            Debug.Log($"State of ${gameObject.name} AI changed from {currentState.ToString()} to {newState.ToString()}");
            currentState = newState;       
        
        GetPathToTarget(currentState);

        }        

        movementManager.selectCurrentBehaviour(GetStateBehaviour(currentState));
    }

    string GetStateBehaviour(SD_State state)
    {
        string behaviour = "";
        switch (state)
        {
            case SD_State.Idle:
                behaviour = "Idle";
                break;
            case SD_State.Seek:
                behaviour = "SeekFly";
                break;
            case SD_State.Charge:
                behaviour = "Seek";
                break;
            case SD_State.Moving:
                behaviour = "SeekFly";
                break;
            case SD_State.Patroling:
                behaviour = "SeekFly";
                break;
            default:
                break;
        }
        return behaviour;
    }

    void GetPathToTarget(SD_State state)
    {
        if (state == SD_State.Moving)
        {
            currentPath = manager.getPathToTarget(info, currentTarget);
            if (currentPath.Count > 0) currentTarget = currentPath[0].GetInfo;
            else currentTarget = manager.getPlayer().GetInfo;
        } else
        {
            currentTarget = player;
        }
    }

    void Explode()
    {
        Debug.Log("EXPLOSION");
        Die();


    }






}
