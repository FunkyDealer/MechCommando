using System;
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
    [SerializeField]
    W_State currentState;

    DTCondition initiateCon;
    DTCondition moveCon;
    DTCondition shootCon;

    Animator animator;

    Transform shootPlace;

    [SerializeField]
    GameObject shotPrefab;

    float shootTimer;
    [SerializeField]
    float shootTime;


    [SerializeField]
    Patrol assignedPatrol;
    [SerializeField]
    bool patrols;

    //  [SerializeField]
    //  AnimationCurve velocity = new AnimationCurve();

    protected override void Awake()
    {
        base.Awake();

        animator = GetComponentInChildren<Animator>();
        animator.SetBool("Walking", false);

        shootPlace = transform.Find("ShootPlace").transform; 
        
        DTAction _ATTACK = new DTAction(() => changeState(W_State.SeekNear));
        DTAction _IDLE = new DTAction(() => checkForIdleOrPatrol());
        DTAction _MOVE = new DTAction(() => changeState(W_State.Moving));
        //DTAction _Shoot 
        //DTCondition _Shoot = new DTCondition(() => Vector3.Distance(info.position, currentTarget.transform.position) < 50, Disparar, _IDLE);
        moveCon = new DTCondition(() => CheckForSeek(), _MOVE, _ATTACK);
        initiateCon = new DTCondition(() => checkForInitiation(), moveCon, _IDLE);
        // dtConditions.Add(attackPlayer);

        shootTimer = 0;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        StartCoroutine(GetAssignedPatrol(1));
        
        
    }

    IEnumerator GetAssignedPatrol(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (patrols && assignedPatrol == null) assignedPatrol = manager.GetClosestPatrol(this.info);
    }

    bool canShoot()
    {
        if (currentState == W_State.SeekNear)
        {
            if (shootTimer <= 0)
            {
                shootTimer = shootTime;
                return true;
            } else
            {
                shootTimer -= Time.deltaTime;
                return false;
            }
        }

        return false;
    }

    void shoot()
    {
        GameObject o = Instantiate(shotPrefab, shootPlace.position, Quaternion.identity);
        PhysLaser l = o.GetComponent<PhysLaser>();
        l.damage = damageOutput;
        l.direction = shootPlace.forward;
        //l.start

    }

    void checkForIdleOrPatrol()
    {
        if (patrols && assignedPatrol != null) changeState(W_State.Patroling);
        else changeState(W_State.Idle);
    }

    bool checkForInitiation()
    {
        return checkForIdle() && AuxMethods.CompareDistanceBigger(info.position, player.position, radarRange);
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



    // Update is called once per frame
    protected override void Update()
    {
       // base.Update();

        if (checkForIdle()) initiateCon.Task();
        else
        {
            moveCon.Task();
        }

        if (info.velocity.magnitude > 30) animator.SetBool("Walking", true);
        else  animator.SetBool("Walking", false);

        //   velocity.AddKey(Time.realtimeSinceStartup, info.velocity.magnitude);
        if (canShoot()) shoot();

        movementManager.Run(currentTarget, info, speed);
    }

    public override void Die()
    {
        base.Die();
    }

    bool checkForIdle()
    {
        return currentState == W_State.Idle || currentState == W_State.Patroling;
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
                currentPath.Clear();
                behaviour = "Idle";
                break;
            case W_State.SeekNear:
                currentPath.Clear();
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
        if (state == W_State.Moving)
        {
            currentPath = manager.getPathToTarget(info, currentTarget);
            if (currentPath.Count > 0) currentTarget = currentPath[0].GetInfo;
            else currentTarget = manager.getPlayer().GetInfo;
        } else if (state == W_State.Patroling)
        {
            currentPath = manager.getPathToTarget(info, assignedPatrol.furthestNode(this.info));
            if (currentPath.Count > 0) currentTarget = currentPath[0].GetInfo;
            else currentPath = manager.getPathToTarget(info, assignedPatrol.furthestNode(this.info));
        }
        else
        {
            currentTarget = player;
        }
    }


    public override void GetNextPathTarget()
    {
        if (currentPath.Count > 1)
        {
            PFNode oldTarget = currentPath[0];
            currentTarget = currentPath[1].GetInfo;
            currentPath.Remove(oldTarget);
        }
        else if (currentPath.Count == 1)
        {
            currentPath.Clear();
            if (currentState != W_State.Patroling) currentTarget = manager.getPlayer().GetInfo;
            else currentPath = manager.getPathToTarget(info, assignedPatrol.furthestNode(this.info));           
        }
        else
        {
            //do nothing
        }
    }
}
