using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuicideDrone :  Enemy
{
    [SerializeField]
    float rangeToAttack;


    protected override void Awake()
    {
        base.Awake();
               

    }



    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        currentTarget = manager.getPlayer();

        movementManager.selectCurrentBehaviour("Idle");
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
        if (Vector3.Distance(transform.position, currentTarget.transform.position) <= rangeToAttack)
        {
            movementManager.selectCurrentBehaviour("Seek");
        }
    }


}
