using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingEntity
{
    [SerializeField]
    protected int damageOutput;
    [SerializeField]
    protected int movementType;
    [SerializeField]
    protected int accuracy;


    //AI
    protected AIMovementManager movementManager;
    protected EnemyManager manager;
    protected Entity currentTarget;

    protected override void Awake()
    {
        base.Awake();

        movementManager = GetComponent<AIMovementManager>();
        EnemyManager.SubcribeSlaves += SubcribeToManager;


        //AI
        currentTarget = null;
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();


    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();


    }


    protected virtual void SubcribeToManager(EnemyManager manager) {

        this.manager = manager;
        manager.Enemies.Add(this);
    }

    public override void Die()
    {
        base.Die();
        manager.Enemies.Remove(this);
    }


    public EnemyManager getManager() => manager;


}
