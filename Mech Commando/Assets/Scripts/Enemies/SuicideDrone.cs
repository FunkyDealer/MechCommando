using System;
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
    DTCondition initiateConShot;
    DTCondition chargeCon;
    DTCondition explodeCon;
    DTCondition moveCon;

    [SerializeField]
    GameObject explosionPrefab;

    AudioSource explosionSound;

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
        initiateConShot = new DTCondition(() => checkForSeek(), _MOVEINTOPOSITION, _SEEK);
        chargeCon = new DTCondition(() => checkForCharge(), _CHARGE, moveCon);
        explodeCon = new DTCondition(() => checkForExplosion(), _EXPLODE, chargeCon);

        GameObject explosionSoundObj = transform.Find("ExplosionFX").gameObject;
        explosionSound = explosionSoundObj.GetComponent<AudioSource>();
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
            case SD_State.Idle:
            case SD_State.Seek:
            case SD_State.Moving:
            case SD_State.Patroling:
                return isPathObstructed(transform.position, player);
        }        
        return false;
    }

    bool checkForCharge()
    {
        if (currentState == SD_State.Seek || currentState == SD_State.Charge)
        {
            float distance = Vector3.Distance(info.position, player.position);
            return AuxMethods.CompareDistanceBigger(info.position, player.position, chargeDis) && !isPathObstructed(transform.position ,player); 
        }
        else return false;
    }

    bool checkForExplosion()
    {
        if (currentState == SD_State.Charge)
        {
            float distance = Vector3.Distance(info.position, player.position);
            return AuxMethods.CompareDistanceBigger(info.position, player.position, ExplosionDis) && !isPathObstructed(transform.position, player); 
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
            if (currentState != SD_State.Charge)
            moveCon.Task();
            chargeCon.Task();
            explodeCon.Task();
        }


        try
        {
            movementManager.Run(currentTarget, info, speed);
        }
        catch (ArgumentNullException)
        {

        }
    }

    public override void Die()
    {
       
        GameObject a = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Explosion e = a.GetComponent<Explosion>();
        e.damage = damageOutput * 3; 
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
                currentPath.Clear();
                behaviour = "Idle";
                break;
            case SD_State.Seek:
                currentPath.Clear();
                behaviour = "SeekFly";
                break;
            case SD_State.Charge:
                currentPath.Clear();
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
        explosionSound.Play();
        Die();
    }

    public override void ReceiveDamage(int damage, Entity shooter)
    {
        base.ReceiveDamage(damage, shooter);
        if (shooter is Player) initiateConShot.Task();
    }




}
